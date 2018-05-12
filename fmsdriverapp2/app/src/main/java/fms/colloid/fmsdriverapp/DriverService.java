package fms.colloid.fmsdriverapp;

import android.app.Service;
import android.content.Intent;
import android.os.IBinder;
import android.widget.Toast;

import java.io.IOException;
import java.io.PrintWriter;
import java.net.Socket;
import java.util.Scanner;

public class DriverService extends Service {

    private Socket conn = null;
    private Scanner in = null;
    private PrintWriter out = null;

    @Override
    public IBinder onBind(Intent intent) {
        // TODO: Return the communication channel to the service.
        throw new UnsupportedOperationException("Not yet implemented");
    }

    @Override
    public int onStartCommand(Intent intent, int flag, int startID) {
        connect("10.0.2.2", 1998);
        log("service started");
        return Service.START_STICKY;
    }

    private void connect(String address, int port) {
        try {
            conn = new Socket(address, port);
            in = new Scanner(conn.getInputStream());
            out = new PrintWriter(conn.getOutputStream());
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private String read() {
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

    private void send(String text) {
        out.write(text);
        out.flush();
    }

    private void log(String message) {
        Toast.makeText(this, message, Toast.LENGTH_LONG).show();
    }
}
