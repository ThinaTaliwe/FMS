package fms.colloid.fmsdriverapp;

import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.ServiceConnection;
import android.os.IBinder;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;

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
    protected void onStart() {
        super.onStart();
        Intent intent = new Intent(this, DriverService.class);
        startService(intent);
        bindService(intent, conn, Context.BIND_AUTO_CREATE);
    }

    @Override
    protected void onDestroy() {
        super.onDestroy();
        unbindService(conn);
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_current_delivery);
        Button current = (Button) findViewById(R.id.accept);
        current.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View view) {
                if(serviceIsBounded) service.send("sf");
                else System.out.println("It doesn't work");
            }
        });
    }
}
