#include <ESP8266WiFi.h>
#include <WiFiClientSecure.h>
#include <ESP8266HTTPClient.h>

#define MQ3_PIN A0
//#define PH_SENSOR_PIN A0

const char* ssid = "Yuriy";
const char* password = "18052002";

double alcoholThreshold = 400;

WiFiClientSecure client;  
HTTPClient http;

const char* host = "https://skincarehelper2024.azurewebsites.net/api/";

void setup() {
  Serial.begin(9600);  
  while (!Serial)
    ;  
  setup_wifi();
}

void loop() {
  if (WiFi.status() == WL_CONNECTED) {
    double sensorValueAlcohol = analogRead(MQ3_PIN);  
    Serial.print("Sensor Value: ");
    Serial.println(sensorValueAlcohol);

    if (sensorValueAlcohol > alcoholThreshold) {
      Serial.println("Alcohol detected!");
    }

    send_state(sensorValueAlcohol);
  } else {
   Serial.println("WiFi Disconnected");
  }
  delay(5000);
}

void setup_wifi() {
  delay(10);
  Serial.println();
  Serial.print("Connecting to ");
  Serial.println(ssid);

  WiFi.begin(ssid, password);

  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }

  Serial.println("");
  Serial.println("WiFi connected");
  Serial.println("IP address: ");
  Serial.println(WiFi.localIP());
}

void send_state(double alcohol) {
  String requestUrl = String(host) + "SensorData";
  Serial.println(requestUrl);
  client.setInsecure();
  http.begin(client, requestUrl);
  http.addHeader("Content-Type", "application/json");

  String body = "{\"phLevel\": 0," + String(" \"alcohol\": ") + alcohol + String(" }");
  Serial.println("POST body: " + body);

  int httpResponseCode = http.POST(body);
  Serial.print("HTTP Response code: ");
  Serial.println(httpResponseCode);

  if (httpResponseCode == 200) {
    Serial.println("Success!");
  } else {
    Serial.print("Error code: ");
    Serial.println(httpResponseCode);
  }
  http.end();
  Serial.println();
}