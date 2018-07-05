package fms.colloid.fmsdriverapp;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.ProgressBar;
import android.widget.TextView;

public class CurrentDelivery extends Base {
    private Button accept;
    private TextView delivery_info;
    private ProgressBar loading;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_current_delivery);
        setControls();
    }

    private void setControls() {
        accept = (Button) findViewById(R.id.accept);
        accept.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View view) {
                if(service.currentDelivery() != null){
                    service.send("accept " + service.currentDelivery().getId());
                    service.sendLocation(service.currentDelivery().getId());
                }
            }
        });
        delivery_info = (TextView) findViewById(R.id.delivery_info);
//        loading = (ProgressBar) findViewById(R.id.progressBar);
//        loading.setVisibility(View.VISIBLE);
//        while (!serviceIsBounded){}
//        loading.setVisibility(View.GONE);
//        if(service.currentDelivery() != null) delivery_info.setText(service.currentDelivery().toString());
//        else delivery_info.setText("No Cuurent delivery");
    }
}
