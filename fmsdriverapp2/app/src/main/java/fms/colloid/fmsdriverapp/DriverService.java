package fms.colloid.fmsdriverapp;

import android.app.Notification;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.app.Service;
import android.app.TaskStackBuilder;
import android.content.Intent;
import android.os.Binder;
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
            in  = null;
            out = null;
        }
    }

    public void send(String text) {
        try {
            System.out.println("Sending: " + text);
            out.write(text + "\n");
            out.flush();
        } catch(Exception ex) {
            System.err.println(ex);
        }
    }

    public boolean availaible() {
        try {
            return conn.getInputStream().available() > 0;
        } catch(Exception ex) {
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
        } catch(ConnectException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        } catch(Exception e) {
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
        } catch(IOException ex){
            alive = false;
        }
        return alive;
    }

    public boolean isAlive() {return isAlive(address, port);}

    public void connect() {
        try{
            if(conn != null && conn.isConnected()) disconnect();
            if(isAlive(address, port)) connect(address, port);
        }catch (Exception ex) {
            ex.printStackTrace();
        }
    }

    private class ServerCheck extends TimerTask {
        @Override
        public void run() {
            tHandler.post(new Runnable() {
                @Override
                public void run() {
                    if(verified() && availaible()) {
                        String text = read();
                        if(text != null) {
                            notification("delivery", text, new Intent(DriverService.this, CurrentDelivery.class));
                        }
                    }
                }
            });
        }
    }

    @Override
    public IBinder onBind(Intent intent) {
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

    @Override
    public void onDestroy() {
        super.onDestroy();
    }

    @Override
    public boolean onUnbind(Intent intent) {
        return super.onUnbind(intent);
    }

    @Override
    public void onRebind(Intent intent) {
        super.onRebind(intent);
    }

    public void log(String message) {
        System.out.println("log: " + message);
        Toast.makeText(this, message, Toast.LENGTH_LONG).show();
    }

    @Override
    public void onCreate() {
        super.onCreate();
        timer.scheduleAtFixedRate(new ServerCheck(), 3000, 5000);
    }
}
