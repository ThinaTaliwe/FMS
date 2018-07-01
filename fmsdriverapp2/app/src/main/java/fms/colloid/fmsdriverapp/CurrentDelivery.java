package fms.colloid.fmsdriverapp;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;

public class CurrentDelivery extends Base {
    private Button accept;
    private EditText delivery_info;

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
                if(service.currentDelivery() != null) service.send("accept " + service.currentDelivery().getId());
            }
        });

        delivery_info = (EditText) findViewById(R.id.delivery_info);

    }
}
