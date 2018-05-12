package fms.colloid.fmsdriverapp;

import android.app.Service;
import android.content.Intent;
import android.os.Binder;
import android.os.IBinder;
import android.os.StrictMode;
import android.widget.Toast;

import java.io.IOException;
import java.io.PrintWriter;
import java.net.Socket;
import java.util.Scanner;

import static android.os.StrictMode.setThreadPolicy;

public class DriverService extends Service {

    private IBinder bound = new DriverServiceBound();
    private Socket conn = null;
    private Scanner in = null;
    private PrintWriter out = null;

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

    private void connect(String address, int port) {
        StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().permitAll().build();
        setThreadPolicy(policy);
        try {
            conn = new Socket(address, port);
            in = new Scanner(conn.getInputStream());
            out = new PrintWriter(conn.getOutputStream());
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public String read() {
        return in.nextLine();
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

    private void send(String text) {
        out.write(text);
        out.flush();
    }

    public void log(String message) {
        Toast.makeText(this, message, Toast.LENGTH_LONG).show();
    }
}
