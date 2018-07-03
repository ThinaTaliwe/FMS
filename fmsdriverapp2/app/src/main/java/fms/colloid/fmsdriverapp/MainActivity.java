package fms.colloid.fmsdriverapp;

import android.Manifest;
import android.content.Intent;
import android.location.Location;
import android.location.LocationListener;
import android.os.Build;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;

public class MainActivity extends Base {
    private Button current, upcoming, sos, login;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        setControls();
        if(!hasPermissions(this)) {
            if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.M) {
                requestPermissions(new String[] {
                        Manifest.permission.ACCESS_FINE_LOCATION, Manifest.permission.ACCESS_COARSE_LOCATION, Manifest.permission.INTERNET
                }, 0);
            }
        }
    }

    private LocationListener locationLitener = new LocationListener() {

        @Override
        public void onLocationChanged(Location location) {
            System.out.println(location.getLongitude() + ":" + location.getLatitude());
        }

        @Override
        public void onStatusChanged(String s, int i, Bundle bundle) {
            System.out.println("status changed: " + s);
        }

        @Override
        public void onProviderEnabled(String s) {
            System.out.println("provider enabled");
        }

        @Override
        public void onProviderDisabled(String s) {
            System.out.println("provider disabled");
            service.showLocationAlert();
        }
    };

    private void setControls() {
        current = (Button) findViewById(R.id.current);
        current.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent newInt = new Intent(MainActivity.this, CurrentDelivery.class);
                startActivity(newInt);
            }
        });

        sos = (Button) findViewById(R.id.sos);
        sos.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View view) {
                service.connect();
            }
        });

        upcoming = (Button) findViewById(R.id.upcoming);

        login = (Button) findViewById(R.id.login);
        login.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent newInt = new Intent(MainActivity.this, Login.class);
                startActivity(newInt);
            }
        });
    }

}
