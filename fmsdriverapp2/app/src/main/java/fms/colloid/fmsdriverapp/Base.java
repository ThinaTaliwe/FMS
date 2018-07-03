package fms.colloid.fmsdriverapp;

import android.Manifest;
import android.app.ActivityManager;
import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.ServiceConnection;
import android.content.pm.PackageManager;
import android.os.Build;
import android.os.IBinder;
import android.support.v4.app.ActivityCompat;
import android.support.v7.app.AppCompatActivity;

public class Base extends AppCompatActivity {
    public static String OK_CODE = "200 OK";
    public static String ERROR_CODE = "400 ERR";
    protected DriverService service;
    protected boolean serviceIsBounded;
    protected ServiceConnection conn = new ServiceConnection() {
        @Override
        public void onServiceConnected(ComponentName componentName, IBinder iBinder) {
            DriverService.DriverServiceBound bound = (DriverService.DriverServiceBound) iBinder;
            service = bound.getBound();
            serviceIsBounded = true;
        }

        @Override
        public void onServiceDisconnected(ComponentName componentName) {
            serviceIsBounded = false;
        }
    };

    @Override
    protected void onStart() {
        super.onStart();
        Intent intent = new Intent(this, DriverService.class);
        if(!isServiceRunning(DriverService.class)) startService(intent);
        bindService(intent, conn, Context.BIND_AUTO_CREATE);
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
        return ActivityCompat.checkSelfPermission(context, Manifest.permission.INTERNET) == PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission(context, Manifest.permission.ACCESS_FINE_LOCATION) == PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission(context, Manifest.permission.ACCESS_COARSE_LOCATION) == PackageManager.PERMISSION_GRANTED;
    }
}
