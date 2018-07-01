package fms.colloid.fmsdriverapp;

import java.text.DateFormat;
import java.text.SimpleDateFormat;

public class Delivery {
    private int id;
    private String orderNum;
    private String truck;
    private String client;
    private String from;
    private String to; 
    private String material;
    private int load;
    private DateFormat departDay;
    private DateFormat arrivalDay;

    public static Delivery newAssignment(String assignment) {
        String[] parts = assignment.split(";");
        Delivery deliv = new Delivery();
        deliv.id = Integer.parseInt(parts[0].split("=")[1]);
        deliv.orderNum = parts[1].split("=")[1];
        deliv.truck = parts[2].split("=")[1];
        deliv.client = parts[3].split("=")[1];
        deliv.from = parts[4].split("=")[1];
        deliv.to = parts[5].split("=")[1];
        deliv.material = parts[6].split("=")[1];
        deliv.load = Integer.parseInt(parts[0].split("=")[1]);
        deliv.departDay = new SimpleDateFormat(parts[0].split("=")[1]);
        return deliv;
    }

    @Override
    public String toString() {
        return "order num: " + orderNum + "\n" +
                "truck: " + truck  + "\n" +
                "client: " + client  + "\n" +
                "from: " + from  + "\n" +
                "to: " + to  + "\n" +
                "material: " + material  + "\n" +
                "load: " + load  + "\n" +
                "depart day: " + departDay  + "\n";
    }

    public int getId() {
        return id;
    }

    public String getOrderNum() {
        return orderNum;
    }

    public String getTruck() {
        return truck;
    }

    public String getClient() {
        return client;
    }

    public String getFrom() {
        return from;
    }

    public String getTo() {
        return to;
    }

    public String getMaterial() {
        return material;
    }

    public int getLoad() {
        return load;
    }

    public DateFormat getDepartDay() {
        return departDay;
    }

    public DateFormat getArrivalDay() {
        return arrivalDay;
    }
}
