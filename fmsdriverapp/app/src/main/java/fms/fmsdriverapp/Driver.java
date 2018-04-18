package fms.fmsdriverapp;

import java.io.IOException;
import java.io.PrintWriter;
import java.net.InetAddress;
import java.net.Socket;
import java.util.Scanner;

public class Driver {
    /*
     * Driver class, provides functionality to android application
     */
    private Socket conn = null;
    private Scanner in = null;
    private PrintWriter out = null;

    private void connect() {
        try {
            conn = new Socket(InetAddress.getByName("localhost"), 1998);
            in = new Scanner(conn.getInputStream());
            out = new PrintWriter(conn.getOutputStream());
        } catch (IOException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
    }

    public void PUT(String request) {
        connect();
        send(request);
        String[] response = read().split(" ");
        switch (response[0]) {

        }
    }


    public void send(String text) {
        out.write(text);
        out.flush();
    }

    public String read() {
        String text = in.nextLine();
        return text;
    }

}
