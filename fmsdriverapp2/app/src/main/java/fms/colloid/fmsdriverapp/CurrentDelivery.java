package fms.colloid.fmsdriverapp;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.ProgressBar;
import android.widget.TextView;

import java.util.EventListener;

public class CurrentDelivery extends Base {
    private Button accept;
    private TextView delivery_info;
    private ProgressBar loading;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_current_delivery);
    }

    @Override
    protected void onStart() {
        super.onStart();
    }

    @Override
    protected void setControls() {
        accept = (Button) findViewById(R.id.accept);
        accept.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View view) {
                if(service.currentDelivery() != null){
                    service.send("accept " + service.currentDelivery().getId());
                    service.sendLocation(service.currentDelivery().getId());
                    service.currentDelivery().accept();
                    startActivity(new Intent(CurrentDelivery.this, Trip.class));
                }
            }
        });
        delivery_info = (TextView) findViewById(R.id.info);
        if(serviceIsBounded && service.currentDelivery() != null) {
            delivery_info.setText(service.currentDelivery().toString());
        } else delivery_info.setText("No current delivery");
    }
}
