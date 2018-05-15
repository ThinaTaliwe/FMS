package fms.colloid.fmsdriverapp;

import android.content.ComponentName;
import android.content.ServiceConnection;
import android.os.IBinder;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;

public class CurrentDelivery extends AppCompatActivity {
    private DriverService service;
    private boolean serviceIsBounded;
    private ServiceConnection conn = new ServiceConnection() {
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
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_current_delivery);

    }
}
