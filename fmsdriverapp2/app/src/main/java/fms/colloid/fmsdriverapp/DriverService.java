package fms.colloid.fmsdriverapp;

import android.Manifest;
import android.app.Activity;
import android.app.AlertDialog;
import android.app.Notification;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.app.Service;
import android.app.TaskStackBuilder;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.location.*;
import android.os.Binder;
import android.os.Bundle;
import android.os.Handler;
import android.os.IBinder;
import android.os.StrictMode;
import android.provider.Settings;
import android.support.v4.app.ActivityCompat;
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

    private IBinder bound = new DriverServiceBound();
    private Socket conn = null;
    private Scanner in = null;
    private PrintWriter out = null;
    private String driver = null;
    private Delivery delivery = null;
    private Timer timer = new Timer();
    private Handler tHandler = new Handler();
    private String address = "10.0.2.2";
    private int port = 1998;
    private LocationManager locationManager;
    private String longitude, latitude;

    @Override
    public void onCreate() {
        super.onCreate();
    }

    public String getDriver() {
        return driver;
    }

    public void setDriver(String driver) {
        this.driver = driver;
    }

    public boolean verified() {
        return !(driver == null);
    }

    public void disconnect() {
        try {
            conn.close();
        } catch (IOException e) {
            e.printStackTrace();
        } finally {
            conn = null;
            in = null;
            out = null;
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
                .setAutoCancel(false)
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
                    System.out.println("servercheck");
                    try {
                        if (verified() && available()) {
                            if (delivery != null) sendLocation(delivery.getId());
                            else System.out.println("null delivery");
                            String text = read();
                            if (text != null) {
                                String[] parts = text.split(" ");
                                switch (parts[0]) {
                                    case "assignment":
                                        delivery = Delivery.newAssignment(parts[1] + " " + parts[2]);
                                        notification("New Assignment", delivery.toString(), new Intent(DriverService.this, CurrentDelivery.class));
                                        sendLocation(delivery.getId());
                                    default:
                                        log(text);
                                        break;
                                }
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
            send("location " + id + " " + longitude + ":" + latitude);
        } else log("location is null");
    }

    private LocationListener locationLitener = new LocationListener() {

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
        public void onProviderDisabled(String s) {
            showLocationAlert();
        }
    };

    public void showLocationAlert() {
        final AlertDialog.Builder dialog = new AlertDialog.Builder(this);
        dialog.setTitle("Enable Location")
                .setMessage("Your Locations Settings is set to 'Off'.\nPlease Enable Location to " +
                        "use this app")
                .setPositiveButton("Location Settings", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface paramDialogInterface, int paramInt) {
                        Intent myIntent = new Intent(Settings.ACTION_LOCATION_SOURCE_SETTINGS);
                        startActivity(myIntent);
                    }
                })
                .setNegativeButton("Cancel", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface paramDialogInterface, int paramInt) {
                    }
                });
        dialog.show();
    }

    public boolean hasPermissions(Context context) {
        boolean access = false;
        if (android.os.Build.VERSION.SDK_INT >= android.os.Build.VERSION_CODES.M) {
            access = checkSelfPermission(Manifest.permission.INTERNET) == PackageManager.PERMISSION_GRANTED && checkSelfPermission(Manifest.permission.ACCESS_FINE_LOCATION) == PackageManager.PERMISSION_GRANTED && checkSelfPermission(Manifest.permission.ACCESS_COARSE_LOCATION) == PackageManager.PERMISSION_GRANTED;
        } else access = true;
        if(access && locationManager == null) {
            locationManager = (LocationManager) getSystemService(LOCATION_SERVICE);
            locationManager.requestLocationUpdates(LocationManager.GPS_PROVIDER, 0, 0, locationLitener);
            timer.scheduleAtFixedRate(new ServerCheck(), 3000, 10000);
        }
        return access;
    }

    @Override
    public IBinder onBind(Intent intent) {
        hasPermissions(this);
        return bound;
    }

    @Override
    public int onStartCommand(Intent intent, int flag, int startID) {
        return Service.START_STICKY;
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

}
