package fms.colloid.fmsdriverapp;

import java.text.DateFormat;

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

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getOrderNum() {
        return orderNum;
    }

    public void setOrderNum(String orderNum) {
        this.orderNum = orderNum;
    }

    public String getTruck() {
        return truck;
    }

    public void setTruck(String truck) {
        this.truck = truck;
    }

    public String getClient() {
        return client;
    }

    public void setClient(String client) {
        this.client = client;
    }

    public String getFrom() {
        return from;
    }

    public void setFrom(String from) {
        this.from = from;
    }

    public String getTo() {
        return to;
    }

    public void setTo(String to) {
        this.to = to;
    }

    public String getMaterial() {
        return material;
    }

    public void setMaterial(String material) {
        this.material = material;
    }

    public int getLoad() {
        return load;
    }

    public void setLoad(int load) {
        this.load = load;
    }

    public DateFormat getDepartDay() {
        return departDay;
    }

    public void setDepartDay(DateFormat departDay) {
        this.departDay = departDay;
    }

    public DateFormat getArrivalDay() {
        return arrivalDay;
    }

    public void setArrivalDay(DateFormat arrivalDay) {
        this.arrivalDay = arrivalDay;
    }

}
