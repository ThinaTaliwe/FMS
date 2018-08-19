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
import android.os.Bundle;
import android.support.v4.app.ActivityCompat;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.PopupWindow;

import java.util.ArrayList;
import java.util.List;

public class Trip extends Base implements OnMapReadyCallback {

    private static final String MAPVIEW_BUNDLE_KEY = "MapViewBundleKey";

    private MapView map;
    private Button info, startEnd;

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
        LatLng dest = service.currentDelivery().getToCoords();
        map.addMarker(new MarkerOptions().position(dest).title("Destination"));
        //map.moveCamera(CameraUpdateFactory.newLatLngZoom(delmas, 10));
        map.getUiSettings().setMapToolbarEnabled(true);
        map.getUiSettings().setZoomControlsEnabled(true);
        map.getUiSettings().isMyLocationButtonEnabled();
        map.moveCamera(CameraUpdateFactory.newLatLng(dest));
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
        try {
            startEnd = (Button) findViewById(R.id.start_end_trip);
            final String strStart = service.currentDelivery().started() ? "End Trip" : "Start Trip";
            startEnd.setText(strStart);
            startEnd.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View view) {
                    try {
                        Delivery deliv = service.currentDelivery();
                        if(!deliv.started()) {
                            service.startDelivery(deliv);
                        } else service.completeDelivery(deliv);
                        service.sendLocation(deliv.getId());
                        startEnd.setText("End Trip");
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
                        if(inflater == null) inflater = (LayoutInflater) getSystemService(Context.LAYOUT_INFLATER_SERVICE);
                        View layout = inflater.inflate(R.layout.trip_control, null);

                        final Button deliv, getRoute, viewRoute;
                        deliv = (Button) layout.findViewById(R.id.deliv_info);
                        getRoute = (Button) layout.findViewById(R.id.get_route);
                        viewRoute = (Button) layout.findViewById(R.id.view_route);

                        deliv.setOnClickListener(new View.OnClickListener() {
                            @Override
                            public void onClick(View view) {
                                try {
                                    showInfo(service.currentDelivery().toString());
                                }
                                catch(Exception ex) {
                                    ex.printStackTrace();
                                }
                            }
                        });

                        getRoute.setOnClickListener(new View.OnClickListener() {
                            @Override
                            public void onClick(View view) {
                                try {
                                    showLoading();
                                    Delivery delivery = service.currentDelivery();
                                    service.send(delivery.getRouteRequest(service));
                                    String response = service.read();
                                    if(response.contains(DriverService.OK_CODE)) response = service.read();
                                    delivery.setRoute(response);
                                    map.getMapAsync(Trip.this);
                                    if(delivery.hasRoute()) {
                                        service.log("Route Added to Map");
                                    } else service.log("Route not added to map, error occurred");
                                    dismiss();
                                }
                                catch(Exception ex) {
                                    ex.printStackTrace();
                                }
                            }
                        });

                        viewRoute.setOnClickListener(new View.OnClickListener() {
                            @Override
                            public void onClick(View view) {
                                try {
                                    String text;
                                    if(service.currentDelivery().hasRoute()) text = service.currentDelivery().getRouteDirections();
                                    else text = "No current delivery\n Click Get Route to request a route";
                                    showInfo(text);
                                }
                                catch(Exception ex) {
                                    ex.printStackTrace();
                                }
                            }
                        });

                        popup = new PopupWindow(layout, 370, 400, true);
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

    public MapView getMap() {return map; }

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