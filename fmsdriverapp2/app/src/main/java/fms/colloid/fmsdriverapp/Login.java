package fms.colloid.fmsdriverapp;

import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;

public class Login extends Base {
    private Button login;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);
        setControls();
    }

    private void setControls() {
        login = (Button) findViewById(R.id.login);
        login.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                String name, pass;
                EditText txtName, txtPass;
                txtName = (EditText) findViewById(R.id.id);
                txtPass = (EditText) findViewById(R.id.password);
                name = txtName.getText().toString();
                pass = txtPass.getText().toString();
                if(service.isAlive() && !service.verified()) {
                    service.connect();
                    service.send(name + " " + pass);
                    String response = service.read();
                    if(response.contains(OK_CODE)) {
                        service.setDriver(name);
                        service.log("login successful");
                    }
                }
                if(service.verified()) {
                    finish();
                } else {
                    service.log("login unsuccessful");
                }
            }
        });

    }
}
