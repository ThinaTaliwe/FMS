package fms.colloid.fmsdriverapp;

import android.content.Intent;
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

    @Override
    protected void setControls() {
        login = (Button) findViewById(R.id.login);
        login.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                try {
                    String name, pass;
                    EditText txtName, txtPass;
                    txtName = (EditText) findViewById(R.id.id);
                    txtPass = (EditText) findViewById(R.id.password);
                    name = txtName.getText().toString().trim();
                    pass = txtPass.getText().toString();
                    if(!service.verified()) {
                        if(service.isAlive()) {
                            showLoading();
                            service.connect();
                            service.send(name + " " + pass);
                            String response = service.read();
                            if(response.contains(DriverService.OK_CODE)) {
                                service.setDriver(name, pass);
                                service.log("Login Successful");
                            } else service.log("Login Unsuccessful");
                            dismiss();
                        } else service.log("No Internet");
                    }
                } catch (Exception ex) {
                    ex.printStackTrace();
                }
                if(service.verified()) startActivity(new Intent(Login.this, MainActivity.class));
            }
        });

    }
}
