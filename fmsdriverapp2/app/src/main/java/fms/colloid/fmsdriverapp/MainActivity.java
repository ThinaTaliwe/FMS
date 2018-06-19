package fms.colloid.fmsdriverapp;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;

public class MainActivity extends Base {
    private Button current, ucoming, sos;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        setControls();
    }

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
                Intent newInt = new Intent(MainActivity.this, Login.class);
                startActivity(newInt);
            }
        });
    }

}
