package fms.colloid.fmsdriverapp;

import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Date;

public class Delivery {
    private int id;
    private String orderNum;
    private String truck;
    private String client;
    private String from;
    private String to; 
    private String material;
    private int load;
    private Date departDay, arrivalDay;
    private boolean accepted, started, completed;
    private static final DateFormat format = new SimpleDateFormat("yyyy/MM/dd HH:mm:ss");
    //2018/07/19 00:00:00

    public static Delivery newAssignment(String assignment) {
        try {
            String[] parts = assignment.split(";");
            Delivery deliv = new Delivery();
            deliv.id = Integer.parseInt(parts[0].split("=")[1]);
            deliv.orderNum = parts[1].split("=")[1];
            deliv.truck = parts[2].split("=")[1];
            deliv.client = parts[3].split("=")[1];
            deliv.from = parts[4].split("=")[1];
            deliv.to = parts[5].split("=")[1];
            deliv.material = parts[6].split("=")[1];
            deliv.load = Integer.parseInt(parts[7].split("=")[1]);
            deliv.departDay = format.parse(parts[8].split("=")[1]);

            return deliv;
        } catch(Exception ex) {
            ex.printStackTrace();
        }
        return null;
    }

    public long locationTimer() {
        if(started) return 300000;
        else if(accepted) return 180000;
        else return 10800000;
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
                "depart day: " + departDay.toString()  + "\n";
    }

    public void complete() { completed = true; }

    public boolean completed() { return completed; }

    public void start() { started = true; }

    public boolean started() { return started; }

    public void accept() { accepted = true; }

    public boolean accepted() { return accepted; }

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

    public Date getDepartDay() {
        return departDay;
    }

    public Date getArrivalDay() {
        return arrivalDay;
    }
}
