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
import android.content.Context;
import android.content.ServiceConnection;
import android.content.pm.PackageManager;
import android.graphics.Color;
import android.location.Geocoder;
import android.os.Bundle;
import android.support.v4.app.ActivityCompat;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.PopupWindow;

import java.util.ArrayList;
import java.util.List;
import java.util.Locale;

public class Trip extends Base implements OnMapReadyCallback {

    private static final String MAPVIEW_BUNDLE_KEY = "MapViewBundleKey";

    private MapView map;
    private Button info;
    private PopupWindow popupWindow;


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
        loading = showLoading();
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
        map.getUiSettings().setZoomControlsEnabled(true);
        try {
            LatLng dest = service.currentDelivery().getToCoords();
            map.addMarker(new MarkerOptions().position(dest).title(service.currentDelivery().getTo()));
            map.moveCamera(CameraUpdateFactory.newLatLngZoom(dest, 10));
            map.getUiSettings().setMapToolbarEnabled(true);
            map.getUiSettings().setZoomControlsEnabled(true);
            map.getUiSettings().isMyLocationButtonEnabled();
            map.moveCamera(CameraUpdateFactory.newLatLng(dest));
            Delivery deliv = service.currentDelivery();
            if (deliv.hasRoute()) {
                addRoute(map, deliv.getPolylines());
            }
        } catch (Exception ex) {
            ex.printStackTrace();
            System.err.println("destination not placed on map");
        }
        dismiss();
    }

    public void addRoute(GoogleMap map, ArrayList<LatLng> polyline) {
        try {
            if (polyline == null) return;
            PolylineOptions options = new PolylineOptions().width(10).color(Color.BLACK).geodesic(true);
            Polyline line = map.addPolyline(options);
            line.setPoints(polyline);
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }

    public String unPad(String text) {
        return text.replace('_', ' ').replace('#', ' ');
    }

    @Override
    protected void setControls() {
        try {

            info = (Button) findViewById(R.id.info);
            info.setOnClickListener(new View.OnClickListener() {

                @Override
                public void onClick(View view) {
                    try {
                        if (inflater == null)
                            inflater = (LayoutInflater) getSystemService(Context.LAYOUT_INFLATER_SERVICE);
                        View layout = inflater.inflate(R.layout.trip_control, null);

                        Delivery delivery = service.currentDelivery();
                        final EditText delivInfo = (EditText) layout.findViewById(R.id.deliv_info);
                        delivInfo.setText(delivery.toString());
                        final Button startEnd;
                        startEnd = (Button) layout.findViewById(R.id.start_end);
                        if (service.currentDelivery().started()) startEnd.setText("End Trip");
                        else startEnd.setText("Start Trip");

                        startEnd.setOnClickListener(new View.OnClickListener() {

                            @Override
                            public void onClick(View view) {
                                try {
                                    Delivery deliv = service.currentDelivery();
                                    if (!deliv.started()) {
                                        service.startDelivery(deliv);
                                        startEnd.setText("End Delivery");
                                    } else {
                                        service.completeDelivery(deliv);
                                    }
                                    service.sendLocation(deliv.getId());
                                } catch (Exception ex) {
                                    ex.printStackTrace();
                                }
                            }
                        });

                        popup = new PopupWindow(layout, 800, 1000, true);
                        popup.showAtLocation(layout, Gravity.CENTER, 0, 0);
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                }
            });
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }

    public MapView getMap() {
        return map;
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