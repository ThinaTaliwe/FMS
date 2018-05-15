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
import java.net.Socket;
import java.net.UnknownHostException;
import java.util.Scanner;
import java.util.Timer;
import java.util.TimerTask;

import static android.os.StrictMode.setThreadPolicy;

public class DriverService extends Service {

    private IBinder bound = new DriverServiceBound();
    private Socket conn = null;
    private Scanner in = null;
    private PrintWriter out = null;
    private Delivery delivery = null;
    private boolean connected;
    private Timer timer = new Timer();
    private Handler tHandler = new Handler();

    private class ServerCheck extends TimerTask {
        @Override
        public void run() {
            tHandler.post(new Runnable() {
                @Override
                public void run() {
                    String text = read();
                    System.out.println(text);
                    if(text != null) notification("delivery", text);
                }
            });
        }
    }

    public void GET(String request) {

    }

    public void verify() {

    }

    private void send(String text) {
        out.write(text + "\n");
        out.flush();
    }

    public String read() {
        try {
            System.out.println(conn.getInputStream().available());
            if(conn.getInputStream().available() > 0) {
                return in.nextLine();
            }
            else return null;
        } catch (IOException e) {
            e.printStackTrace();
        }
        return null;
    }

    public void notification(String title, String content) {
        Intent intent = new Intent(this, MainActivity.class);
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
            connected = true;
            send("1234567770123");;
            timer.scheduleAtFixedRate(new ServerCheck(), 0, 1000);
            log("Service started");
        } catch(UnknownHostException e) {
            log("Service not connected");
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public void log(String message) {
        Toast.makeText(this, message, Toast.LENGTH_LONG).show();
    }

    public boolean connected() { return connected;}

    @Override
    public IBinder onBind(Intent intent) {
        return bound;
    }

    @Override
    public int onStartCommand(Intent intent, int flag, int startID) {
        connect("10.0.2.2", 1998);
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
        log("service stopped");
    }

    @Override
    public boolean onUnbind(Intent intent) {
        return super.onUnbind(intent);
    }

    @Override
    public void onRebind(Intent intent) {
        super.onRebind(intent);
    }
}
