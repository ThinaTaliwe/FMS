package fms.fmsdriverapp;

import android.widget.TextView;

import org.w3c.dom.Text;

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
    private String address = null;
    private int port;


    public Driver(String address, int port) {
        this.address = address;
        this.port = port;
        connect(address, port);
    }

    private void connect(String address, int port) {
        try {
            conn = new Socket(InetAddress.getByName(address), port);
            in = new Scanner(conn.getInputStream());
            out = new PrintWriter(conn.getOutputStream());
        } catch (IOException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
    }

    public boolean verify() {
        return false;
    }

    public void PUT(String request) {
        send(request);
        String[] response = read().split(" ");
        switch (response[0]) {
            case "":

                break;
        }
    }

    public void GET(String request) {
        send(request);
        String[] response = read().split(" ");
        switch (response[0]) {
            case "":

                break;
        }
    }

    public void POST(String request) {
        send(request);
        String[] response = read().split(" ");
        switch (response[0]) {
            case "":

                break;
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
