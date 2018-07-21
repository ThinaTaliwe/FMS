package fms.colloid.fmsdriverapp;

import android.Manifest;
import android.app.Notification;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.app.Service;
import android.app.TaskStackBuilder;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.location.*;
import android.os.Binder;
import android.os.Bundle;
import android.os.Handler;
import android.os.IBinder;
import android.os.StrictMode;
import android.widget.Toast;

import java.io.IOException;
import java.io.PrintWriter;
import java.net.ConnectException;
import java.net.InetSocketAddress;
import java.net.Socket;
import java.util.Scanner;
import java.util.Timer;
import java.util.TimerTask;

import static android.os.StrictMode.setThreadPolicy;

public class DriverService extends Service {

    public static final String OK_CODE = "200 OK";
    public static final String ERROR_CODE = "400 ERR";

    private IBinder bound = new DriverServiceBound();
    private Socket conn = null;
    private Scanner in = null;
    private PrintWriter out = null;
    private String[] driver = null;
    private Delivery delivery = null;
    private Timer timer = null;
    private Handler tHandler = new Handler();
    private String address = "197.228.215.67"; //10.0.2.2
    private int port = 1998;
    private LocationManager locationManager;
    private String longitude, latitude;
    private ServerCheck serverCheck = new ServerCheck();

    @Override
    public void onCreate() {
        super.onCreate();
    }

    public String getDriver() {
        return driver[0];
    }

    public void setDriver(String driver, String pass) {
        this.driver = new String[]{driver, pass};
    }

    public boolean verified() {
        return !(driver == null);
    }

    public void disconnect() {
        try {
            send("kill");
            conn.close();
        } catch (IOException e) {
            e.printStackTrace();
        } finally {
            conn = null;
            in = null;
            out = null;
        }
    }

    public void logOff() {
        try {
            disconnect();
        } catch (Exception ex) {
            ex.printStackTrace();
        } finally {
            driver = null;
        }
    }

    public void reconnect() {
        if(isAlive()) {
            if(verified()) {
                disconnect();
                connect();
                log(read());
                send(driver[0] + " " + driver[1]);
                String response = read();
                if(response.contains(OK_CODE)) {
                    send("current");
                    response = read();
                    if(response != null)
                        delivery = Delivery.newAssignment(response);
                }
            } else log("Login to view current deliveries");
        }
    }

    public void send(String text) {
        try {
            System.out.println("Sending: " + text);
            out.write(text + "\n");
            out.flush();
        } catch (Exception ex) {
            System.err.println(ex);
        }
    }

    public boolean available() {
        try {
            return conn.getInputStream().available() > 0;
        } catch (Exception ex) {
            ex.printStackTrace();
        }
        return false;
    }

    public String read() {
        try {
            String text = in.nextLine();
            System.out.println("Read: " + text);
            return text;
        } catch (Exception e) {
            e.printStackTrace();
        }
        return null;
    }

    public void notification(String title, String content, Intent intent) {
        TaskStackBuilder stackBuilder = TaskStackBuilder.create(this);
        stackBuilder.addNextIntentWithParentStack(intent);
        PendingIntent pIntent = stackBuilder.getPendingIntent(0, PendingIntent.FLAG_UPDATE_CURRENT);
        Notification notif = new Notification.Builder(this)
                .setContentTitle(title)
                .setAutoCancel(true)
                .setSmallIcon(R.drawable.ic_launcher_background)
                .setContentIntent(pIntent)
                .setStyle(new Notification.BigTextStyle().bigText(content)).build();
        NotificationManager notificationManager = (NotificationManager) getSystemService(NOTIFICATION_SERVICE);
        notificationManager.notify(0, notif);
    }

    private void connect(String address, int port) {
        StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
        setThreadPolicy(policy);
        try {
            conn = new Socket(address, port);
            in = new Scanner(conn.getInputStream());
            out = new PrintWriter(conn.getOutputStream());
            log(read());
        } catch (ConnectException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private boolean isAlive(String host, int port) {
        boolean alive = true;
        try {
            StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
            setThreadPolicy(policy);
            InetSocketAddress address = new InetSocketAddress(host, port);
            Socket socket = new Socket();
            socket.connect(address, 1);
            socket.close();
        } catch (IOException ex) {
            alive = false;
        }
        return alive;
    }

    public boolean isAlive() {
        return isAlive(address, port);
    }

    public void connect() {
        try {
            if (conn != null) disconnect();
            if (isAlive(address, port)) connect(address, port);
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }

    public Delivery currentDelivery() {
        return delivery;
    }

    private class ServerCheck extends TimerTask {

        @Override
        public void run() {
            tHandler.post(new Runnable() {
                @Override
                public void run() {
                    try {
                        if(verified()) {
                            if(!conn.isConnected()) reconnect();
                            else if (delivery != null) {
                                sendLocation(delivery.getId());
                            }
                            if(available()) {
                                String text = read();
                                if (text != null) {
                                    String[] parts = text.split(" ");
                                    switch (parts[0]) {
                                        case "assignment":
                                            delivery = Delivery.newAssignment(parts[1] + " " + parts[2]);
                                            notification("New Assignment", delivery.toString(), new Intent(DriverService.this, CurrentDelivery.class));
                                            sendLocation(delivery.getId());
                                            setTimer(5000);
                                            setLocationTimer(5000);
                                            break;
                                        case OK_CODE:
                                            System.out.println(OK_CODE);
                                            break;
                                        default:
                                            log(text);
                                            break;
                                    }
                                }
                            }
                        } else {
                            if(isAlive()) {
                                //("Login into FMS Driver App", "Login to check if have any new deliveries", new Intent(DriverService.this, Login.class));
                            }
                        }
                    } catch (Exception ex) {
                        ex.printStackTrace();
                    }
                }
            });
        }
    }

    public void sendLocation(int id) {
        if (longitude != null && latitude != null) {
            send("location " + id + " " + latitude + ":" + longitude);
        }
    }

    private LocationListener locationListener = new LocationListener() {

        @Override
        public void onLocationChanged(Location location) {
            longitude = String.valueOf(location.getLongitude());
            latitude = String.valueOf(location.getLatitude());
        }

        @Override
        public void onStatusChanged(String s, int i, Bundle bundle) { }

        @Override
        public void onProviderEnabled(String s) { }

        @Override
        public void onProviderDisabled(String s) { }
    };

    public void setLocationTimer(long period) {
        boolean access;
        if (android.os.Build.VERSION.SDK_INT >= android.os.Build.VERSION_CODES.M) {
            access = checkSelfPermission(Manifest.permission.INTERNET) == PackageManager.PERMISSION_GRANTED && checkSelfPermission(Manifest.permission.ACCESS_FINE_LOCATION) == PackageManager.PERMISSION_GRANTED && checkSelfPermission(Manifest.permission.ACCESS_COARSE_LOCATION) == PackageManager.PERMISSION_GRANTED;
        } else access = true;
        if(access) {
            if(locationManager == null) {
                locationManager = (LocationManager) getSystemService(LOCATION_SERVICE);
                Location location = locationManager.getLastKnownLocation(LocationManager.GPS_PROVIDER);
                latitude = String.valueOf(location.getLatitude());
                longitude = String.valueOf(location.getLongitude());
            }
            locationManager.requestLocationUpdates(LocationManager.GPS_PROVIDER, period, 0, locationListener);
        }
    }

    public double[] getLocation() {
        try {
            return new double[] {
                    Double.parseDouble(latitude),
                    Double.parseDouble(longitude)
            };
        } catch (Exception e) {
            e.printStackTrace();
        }
        return new double[] {26, 28};
    }

    @Override
    public IBinder onBind(Intent intent) {
        if(timer == null) setTimer(10000);
        return bound;
    }

    @Override
    public int onStartCommand(Intent intent, int flag, int startID) {
        return Service.START_STICKY;
    }

    public void setTimer(long period) {
        try {
            timer = null;
            timer = new Timer();
            timer.scheduleAtFixedRate(serverCheck, 5000, period);
            if(delivery != null) {
                if(delivery.completed()) return;
                else if(delivery.started()) period = 10000;
                else if (delivery.accepted()) period = 10000;
                else period = 10000;
                setLocationTimer(period);
            } else return;
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }

    public class DriverServiceBound extends Binder {
        DriverService getBound() {
            return DriverService.this;
        }
    }

    public void log(String message) {
        System.out.println("log: " + message);
        Toast.makeText(this, message, Toast.LENGTH_SHORT).show();
    }

    public void acceptDelivery(Delivery delivery) {
        delivery.accept();
        send("accept " + delivery.getId());
    }

    public void startDelivery(Delivery delivery) {
        delivery.start();
        send("start " + delivery.getId());
    }

    public void completeDelivery(Delivery delivery) {
        delivery.complete();
        send("complete " + delivery.getId());
    }

}
