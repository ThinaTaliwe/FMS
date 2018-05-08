package fms.fmsdriverapp;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.widget.TextView;

public class blank extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_blank);
        TextView txt = (TextView) findViewById(R.id.text);
        Driver d = new Driver("localhost", 1998);
        //txt.setText(d.read());
    }
}
