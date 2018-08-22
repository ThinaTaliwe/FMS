package fms.colloid.fmsdriverapp;

import com.google.android.gms.maps.model.LatLng;
import com.google.maps.android.PolyUtil;

import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import org.json.JSONObject;

public class Delivery {
    private int id;
    private String orderNum;
    private String truck;
    private String client;
    private String fromCoords, fromAddress;
    private String toCoords, toAddress;
    private String route;
    private String material;
    private int load;
    private Date departDay, arrivalDay;
    private boolean accepted, started, completed;
    private static final DateFormat format = new SimpleDateFormat("yyyy/MM/dd HH:mm:ss");
    //2018/07/19 00:00:00

    public static Delivery newAssignment(String delivery) {
        try {
            JSONObject json = new JSONObject(delivery);
            Delivery deliv = new Delivery();
            deliv.id = json.getInt("id");
            deliv.orderNum = json.getString("orderNum");
            deliv.truck = json.getString("truck");
            deliv.client = json.getString("client");
            deliv.fromCoords = json.getString("fromCoords");
            deliv.fromAddress = json.getString("fromAddress");
            deliv.toCoords = json.getString("toCoords");
            deliv.toAddress = json.getString("toAddress");
            deliv.material = json.getString("material");
            deliv.load = json.getInt("load");
            deliv.departDay = format.parse(json.getString("departDay"));
            return deliv;
        } catch(Exception ex) {
            ex.printStackTrace();
            System.err.println("Invalid delivery " + delivery);
        }
        return null;
    }

    public LatLng getToCoords() {
        try {
            String[] parts = toCoords.split(":");
            return new LatLng(Double.parseDouble(parts[0]), Double.parseDouble(parts[1]));
        }catch (Exception ex) {

        } return null;
    }

    public LatLng getFromInLatLong() {
        try {
            String[] parts = fromCoords.split(":");
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
                "from: " + fromAddress  + "\n" +
                "to: " + toAddress  + "\n" +
                "material: " + material  + "\n" +
                "load: " + load  + "\n" +
                "depart day: " + departDay.toString()  + "\n";
    }

    public String getRouteRequest(DriverService service) {
        service.clearInputStream();
        String request = "route " + service.getLocation() + " ";
        if(accepted && !started)  request += fromCoords;
        else  request += toCoords;
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
        return fromAddress;
    }

    public String getTo() {
        return toAddress;
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
