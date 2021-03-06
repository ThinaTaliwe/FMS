package fms.colloid.fmsdriverapp;

import android.Manifest;
import android.app.ActivityManager;
import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.ComponentName;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.ServiceConnection;
import android.content.pm.PackageManager;
import android.location.LocationManager;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.IBinder;
import android.provider.Settings;
import android.support.annotation.Nullable;
import android.support.v4.app.ActivityCompat;
import android.support.v7.app.AppCompatActivity;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.LinearLayout;
import android.widget.PopupWindow;
import android.widget.RelativeLayout;
import android.widget.TextSwitcher;
import android.widget.TextView;

public class Base extends AppCompatActivity {

    protected PopupWindow popup;
    protected AlertDialog loading;
    protected LayoutInflater inflater;
    protected DriverService service;
    protected boolean serviceIsBounded;
    protected ServiceConnection conn = new ServiceConnection() {
        /**
         * used to connect to DriverService
         */
        @Override
        public void onServiceConnected(ComponentName componentName, IBinder iBinder) {
            DriverService.DriverServiceBound bound = (DriverService.DriverServiceBound) iBinder;
            service = bound.getBound();
            serviceIsBounded = true;
            setControls();
        }

        @Override
        public void onServiceDisconnected(ComponentName componentName) {
            serviceIsBounded = false;
        }
    };

    protected class Helper extends AsyncTask<String, String, String> {

        private Intent intent;
        private Context context;

        public Helper(Context con) { context = con; }

        @Override
        protected String doInBackground(String... args) {
            try {
                if(args[0] == "trip") {
                    intent = new Intent(Base.this, Trip.class);
                } else if(args[0] == "route") {
                    showLoading();
                    Delivery delivery = service.currentDelivery();
                    service.send("route " + service.getLocation() + " -26.1403:28.6787");
                    String response = service.read();
                    if(response.contains(DriverService.OK_CODE)) response = service.read();
                    delivery.setRoute(response);
                    Trip trip = (Trip) context;
                    trip.getMap().getMapAsync(trip);
                    dismiss();
                }
            } catch (Exception ex) {
                ex.printStackTrace();
            }
            return null;
        }

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
        }

        @Override
        protected void onPostExecute(String s) {
            super.onPostExecute(s);
            if(intent != null) context.startActivity(intent);
        }
    }

    protected void showInfo(String info) {
        /**
         * shows information in a popup
         */
        try {
            System.out.println("viewDeliveryInfo()");
            if(inflater == null) inflater = (LayoutInflater) Base.this.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
            View layout = inflater.inflate(R.layout.route_info, null);
            TextView text = (TextView) layout.findViewById(R.id.routeText);
            text.setText(info);
            popup = new PopupWindow(layout, 1000, 1000, true);
            popup.showAtLocation(layout, Gravity.CENTER, 0, 0);
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }

    protected AlertDialog showLoading() {
        /**
         * shows a loading symbol
         */
        try {
            View view = getLayoutInflater().inflate(R.layout.loading, null, false);
            AlertDialog.Builder alertDialog = new AlertDialog.Builder(this);
            alertDialog.setView(view)
                    .setCancelable(true);
            return alertDialog.show();
        } catch (Exception e) {
            e.printStackTrace();
        }
        return null;
    }

    protected void dismiss() {
        /**
         * removes popup
         */
        if(popup != null) popup.dismiss();
    }

    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        Intent intent = new Intent(Base.this, DriverService.class);
        if(!isServiceRunning(DriverService.class)) startService(intent);
        bindService(intent, conn, Context.BIND_AUTO_CREATE);
    }

    @Override
    protected void onStart() {
        super.onStart();
        LocationManager locationManager = (LocationManager) getSystemService(LOCATION_SERVICE);
        if(!locationManager.isProviderEnabled(LocationManager.GPS_PROVIDER)) {
            showLocationAlert();
        }
    }

    protected void setControls() {
        /**
         * sets controls and has access to service, must be implemented in inherited class
         */
        System.err.println("setControls()");
    }

    @Override
    protected void onDestroy() {
        unbindService(conn);
        super.onDestroy();
    }

    public boolean isServiceRunning(Class serviceClass) {
        ActivityManager manager = (ActivityManager) getSystemService(ACTIVITY_SERVICE);
        for(ActivityManager.RunningServiceInfo service : manager.getRunningServices(Integer.MAX_VALUE)) {
            if(serviceClass.getName().equals(service.service.getClassName())) return true;
        }
        return false;
    }

    public boolean hasPermissions(Context context) {
        return ActivityCompat.checkSelfPermission(context, Manifest.permission.INTERNET) == PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission(context, Manifest.permission.ACCESS_FINE_LOCATION) == PackageManager.PERMISSION_GRANTED;
    }

    public void showLocationAlert() {
        final AlertDialog.Builder dialog = new AlertDialog.Builder(this);
        dialog.setTitle("Enable Location")
                .setMessage("Your Locations Settings is set to 'Off'.\nPlease Enable Location to " +
                        "use this app")
                .setPositiveButton("Location Settings", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface paramDialogInterface, int paramInt) {
                        Intent myIntent = new Intent(Settings.ACTION_LOCATION_SOURCE_SETTINGS);
                        startActivity(myIntent);
                    }
                })
                .setNegativeButton("Cancel", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface paramDialogInterface, int paramInt) {
                    }
                });
        dialog.show();
    }
}
