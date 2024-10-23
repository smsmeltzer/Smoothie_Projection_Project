const int buttonPin = 2;
const int ledPin = 13;

int buttonState = 0;

void setup() {
  Serial.begin(9600);

  pinMode(ledPin, OUTPUT);
  pinMode(buttonPin, INPUT);
}

void loop() {
  if (digitalRead(buttonPin) == HIGH) {
    digitalWrite(ledPin, HIGH);
    Serial.println(1);
    Serial.flush();
    delay(20);
  } else {
    digitalWrite(ledPin, LOW);
    Serial.println(0);
    Serial.flush();
  }
}
