package fms.colloid.fmsdriverapp;

import com.google.android.gms.maps.CameraUpdateFactory;
import com.google.android.gms.maps.GoogleMap;
import com.google.android.gms.maps.MapView;
import com.google.android.gms.maps.OnMapReadyCallback;
import com.google.android.gms.maps.model.LatLng;
import com.google.android.gms.maps.model.MarkerOptions;
import com.google.android.gms.maps.model.Polyline;
import com.google.android.gms.maps.model.PolylineOptions;

import android.Manifest;
import android.content.pm.PackageManager;
import android.graphics.Color;
import android.os.Bundle;
import android.support.v4.app.ActivityCompat;
import android.support.v7.app.AppCompatActivity;

import org.json.JSONArray;
import org.json.JSONObject;

import java.net.URL;
import java.util.ArrayList;
import java.util.Scanner;

public class Trip extends Base implements OnMapReadyCallback {

    private static final String MAPVIEW_BUNDLE_KEY = "MapViewBundleKey";

    private MapView map;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_trip);
        Bundle mapViewBundle = null;
        if (savedInstanceState != null) {
            mapViewBundle = savedInstanceState.getBundle(MAPVIEW_BUNDLE_KEY);
        }
        map = (MapView) findViewById(R.id.map);
        map.onCreate(mapViewBundle);
        map.getMapAsync(this);
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        Bundle mapViewBundle = outState.getBundle(MAPVIEW_BUNDLE_KEY);
        if (mapViewBundle == null) {
            mapViewBundle = new Bundle();
            outState.putBundle(MAPVIEW_BUNDLE_KEY, mapViewBundle);
        }
        map.onSaveInstanceState(mapViewBundle);
    }

    @Override
    public void onMapReady(GoogleMap map) {
        if (ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
            return;
        }
        map.setMyLocationEnabled(true);
        map.setMinZoomPreference(6);
        map.setTrafficEnabled(true);
        map.getUiSettings().setMapToolbarEnabled(true);
        map.getUiSettings().setAllGesturesEnabled(true);
        LatLng delmas = new LatLng(-26.1403, 28.6787);
        map.addMarker(new MarkerOptions().position(delmas).title("Delmas"));
        //map.moveCamera(CameraUpdateFactory.newLatLngZoom(delmas, 10));
        map.getUiSettings().setMapToolbarEnabled(true);
        map.getUiSettings().isMyLocationButtonEnabled();
        map.moveCamera(CameraUpdateFactory.newLatLng(delmas));
        String route = getRoute(service.getLocation(), new double[]{-26.1403, 28.6787});
        String[] routePoints = getRoutePoints(route);
        addRoute(map, routePoints);
    }

    public void addRoute(GoogleMap map, String[] route) {
        if(route == null) return;
        ArrayList<LatLng> points = new ArrayList<>();
        for (int c = 1; c < route.length; c++){
            String[] ss = route[c].split(" ");
            String[] parts = ss[2].split(":");
            double lat = Double.parseDouble(parts[0]);
            double lng = Double.parseDouble(parts[1]);
            points.add(new LatLng(lat, lng));
        }
        PolylineOptions options = new PolylineOptions().width(4).color(Color.BLACK).geodesic(true);
        Polyline line = map.addPolyline(options);
        line.setPoints(points);
    }

    public String[] getRoutePoints(String route) {
        String[] results = null;
        try {
            JSONObject obj = new JSONObject(route);
            JSONArray routes = obj.getJSONArray("routes");
            JSONArray legs = routes.getJSONObject(0).getJSONArray("legs");
            JSONObject info = legs.getJSONObject(0);
            String distance = info.getJSONObject("distance").getString("text");
            JSONArray steps = info.getJSONArray("steps");
            results = new String[steps.length() + 1];
            results[0] = distance;
            JSONObject mark = null;
            String strMark = "";
            for(int c = 1; c <= steps.length(); c++) {
                mark = steps.getJSONObject(c - 1);
                strMark += mark.getJSONObject("distance").getString("text") + " ";
                strMark += mark.getJSONObject("end_location").getDouble("lat") + ":" + mark.getJSONObject("end_location").getDouble("lng");
                results[c] = strMark;
                strMark = "";
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
        return results;
    }

    public String getRoute(double[] from, double[] to) {
        String link = "https://maps.googleapis.com/maps/api/directions/json?mode=driving&origin=";
        link += from[0] + "," + from[1] + "&destination=";
        link += to[0] + "," + to[1] + "&key=AIzaSyChZ0yP0HTxPypmlDNYgkpQMXqQD3UASpw";
        try {
            Scanner in = new Scanner(new URL(link).openStream());
            String response = "";
            while (in.hasNext()) response += in.nextLine() + "\n";
            return response;
        } catch (Exception e) {
            e.printStackTrace();
        }
        return null;
    }

    @Override
    protected void onResume() {
        super.onResume();
        map.onResume();
    }

    @Override
    protected void onStart() {
        super.onStart();
        map.onStart();
    }

    @Override
    protected void onStop() {
        super.onStop();
        map.onStop();
    }

    @Override
    protected void onPause() {
        map.onPause();
        super.onPause();
    }

    @Override
    protected void onDestroy() {
        map.onDestroy();
        super.onDestroy();
    }

    @Override
    public void onLowMemory() {
        super.onLowMemory();
        map.onLowMemory();
    }

}