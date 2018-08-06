package fms.colloid.fmsdriverapp;

import com.google.android.gms.maps.model.LatLng;
import com.google.maps.android.PolyUtil;

import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

public class Delivery {
    private int id;
    private String orderNum;
    private String truck;
    private String client;
    private String from;
    private String to;
    private String route;
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

    public LatLng getToInLatLong() {
        try {
            String[] parts = to.split(":");
            return new LatLng(Double.parseDouble(parts[0]), Double.parseDouble(parts[1]));
        }catch (Exception ex) {

        } return null;
    }

    public LatLng getFromInLatLong() {
        try {
            String[] parts = from.split(":");
            return new LatLng(Double.parseDouble(parts[0]), Double.parseDouble(parts[1]));
        }catch (Exception ex) {

        } return null;
    }

    public long locationTimer() {
        if(started) return 300000;
        else if(accepted) return 180000;
        else return -1;
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

    public String getRouteRequest(DriverService service) {
        service.clearInputStream();
        String request = "route " + service.getLocation() + " ";
        if(accepted && !started)  request += from;
        else  request += to;
        return request;
    }

    public ArrayList<LatLng> getPolylines() {
        String[][] info = getRouteInfo();
        ArrayList<LatLng> polyline = new ArrayList<>();
        for(int c = 1; c < info.length; c++) {
            try {
                List<LatLng> list = PolyUtil.decode(info[c][2]);
                polyline.addAll(list);
            } catch (Exception ex) {
                ex.printStackTrace();
            }
        }
        return polyline;
    }

    public String getRouteDirections() {
        String[][] info = getRouteInfo();
        String result = "";
        for(int c = 1; c < info.length; c++) {
            try {
                result += unPad(info[c][1]) + "\n";
            } catch (Exception ex) {
                ex.printStackTrace();
            }
        }
        return result;
    }

    public String getRouteDistance() {
        try {
            String[][] result = getRouteInfo();
            return unPad(result[0][0]) + "\n";
        } catch (Exception ex) {
            ex.printStackTrace();
        } return null;
    }

    public String[][] getRouteInfo() {
        System.out.println("getRouteInfo()");
        String[][] result = null;
        try {
            String[] parts = route.split(" ");
            result = new String[parts.length][3];
            result[0][0] = unPad(parts[0]);
            for(int c = 1; c < parts.length; c++) {
                result[c] = parts[c].split("#");
            }
        } catch (Exception ex) {
            ex.printStackTrace();
        }
        return result;
    }

    public String unPad(String text) { return text.replace('_', ' ').replace('#',  ' '); }

    public boolean hasRoute() {return !(route == null); }

    public void setRoute(String route) {
        if(route.contains(DriverService.ERROR_CODE) || route.contains(DriverService.OK_CODE) || route.contains(DriverService.SERVER_ERROR)){
            System.out.println("Invalid route given");
        } else {
            this.route = route;
            System.out.println("Route set");
        }
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
