package fms.colloid.fmsdriverapp;

import android.Manifest;
import android.content.Intent;
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
        if(!hasPermissions(this)) {
            if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.M) {
                requestPermissions(new String[] {
                        Manifest.permission.ACCESS_FINE_LOCATION, Manifest.permission.ACCESS_COARSE_LOCATION, Manifest.permission.INTERNET
                }, 0);
            }
        }
    }

    @Override
    protected void setControls() {
        current = (Button) findViewById(R.id.current);
        current.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent newInt = null;
                if(service.currentDelivery() != null && service.currentDelivery().accepted())    {
                    Helper help = new Helper(MainActivity.this);
                    help.execute("trip");
                }
                else {
                    newInt = new Intent(MainActivity.this, CurrentDelivery.class);
                    startActivity(newInt);
                }
            }
        });

        sos = (Button) findViewById(R.id.sos);
        sos.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View view) {
                showInfo(service.getLocation());
            }
        });

        upcoming = (Button) findViewById(R.id.upcoming);
        upcoming.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View view) {
                try {
                    service.connect();
                    service.send("1234567770123 MMELI");
                    service.setDriver("1234567770123", "MMELI");
                    String response = service.read();
                    service.clearInputStream();
                    service.send("current");
                    response = service.read();
                    service.setDelivery(Delivery.newAssignment(response));
                    service.notification("Current delivery", service.currentDelivery().toString(), new Intent(MainActivity.this, Trip.class));
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        });

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
