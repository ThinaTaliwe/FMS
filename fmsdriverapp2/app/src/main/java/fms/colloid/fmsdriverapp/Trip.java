package fms.colloid.fmsdriverapp;

import com.google.android.gms.maps.CameraUpdateFactory;
import com.google.android.gms.maps.GoogleMap;
import com.google.android.gms.maps.MapView;
import com.google.android.gms.maps.OnMapReadyCallback;
import com.google.android.gms.maps.model.LatLng;
import com.google.android.gms.maps.model.MarkerOptions;
import com.google.android.gms.maps.model.Polyline;
import com.google.android.gms.maps.model.PolylineOptions;
import com.google.maps.android.PolyUtil;

import android.Manifest;
import android.app.Service;
import android.content.ServiceConnection;
import android.content.pm.PackageManager;
import android.graphics.Color;
import android.os.Bundle;
import android.support.v4.app.ActivityCompat;
import android.view.View;
import android.widget.Button;

import java.util.ArrayList;
import java.util.List;

public class Trip extends Base implements OnMapReadyCallback {

    private static final String MAPVIEW_BUNDLE_KEY = "MapViewBundleKey";

    private MapView map;
    private Button info, getRoute, viewRoute;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_trip);
        showLoading();
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
        System.out.println("getMapAsync()");
        if (ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
            return;
        }
        map.setMyLocationEnabled(true);
        map.setMinZoomPreference(8);
        map.setTrafficEnabled(true);
        map.getUiSettings().setMapToolbarEnabled(true);
        map.getUiSettings().setAllGesturesEnabled(true);
        LatLng delmas = new LatLng(-26.1403, 28.6787);
        map.addMarker(new MarkerOptions().position(delmas).title("Delmas"));
        //map.moveCamera(CameraUpdateFactory.newLatLngZoom(delmas, 10));
        map.getUiSettings().setMapToolbarEnabled(true);
        map.getUiSettings().isMyLocationButtonEnabled();
        map.moveCamera(CameraUpdateFactory.newLatLng(delmas));
        try {
            Delivery deliv = service.currentDelivery();
            if(deliv.hasRoute()) {
                addRoute(map, deliv.getPolylines());
            }
        } catch (Exception ex) {
            ex.printStackTrace();
        }
        dismiss();
    }

    public void addRoute(GoogleMap map, ArrayList<LatLng> polyline) {
        try {
            if(polyline == null) return;
            PolylineOptions options = new PolylineOptions().width(10).color(Color.BLACK).geodesic(true);
            Polyline line = map.addPolyline(options);
            line.setPoints(polyline);
        } catch(Exception ex) {
            ex.printStackTrace();
        }
    }

    public String[][] getRouteInfo(String route) {
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

    @Override
    protected void setControls() {
        getRoute = (Button) findViewById(R.id.getRoute);
        getRoute.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View view) {
                try {
                    service.clearInputStream();
                    service.send("route " + service.getLocation() + " -26.1403:28.6787");
                    String response = service.read();
                    if(response.contains(DriverService.OK_CODE)) response = service.read();
                    service.currentDelivery().setRoute(response);
                    map.getMapAsync(Trip.this);
                } catch (Exception ex) {
                    ex.printStackTrace();
                }
            }
        });

        viewRoute = (Button) findViewById(R.id.viewRoute);
        viewRoute.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View view) {
                try {
                    Delivery deliv = service.currentDelivery();
                    showInfo(deliv.getRouteDistance() + deliv.getRouteDirections());
                } catch (Exception ex) {
                    ex.printStackTrace();
                }
            }
        });

        info = (Button) findViewById(R.id.info);
        info.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View view) {
                try {
                    showInfo(service.currentDelivery().toString());
                } catch (Exception ex) {
                    ex.printStackTrace();
                }
            }
        });

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