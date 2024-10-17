const int buttonPin1 = 2;
const int buttonPin2 = 3;
const int buttonPin3 = 4;
const int buttonPin4 = 5;

const int ledPin1 = 10;
const int ledPin2 = 11;
const int ledPin3 = 12;
const int ledPin4 = 13;

// Tracks if button is pressed
bool button1_state = false;
bool button2_state = false;
bool button3_state = false;
bool button4_state = false;

void setup() {
  Serial.begin(9600);

  pinMode(buttonPin1, INPUT);
  pinMode(buttonPin2, INPUT);
  pinMode(buttonPin3, INPUT);
  pinMode(buttonPin4, INPUT);

  pinMode(ledPin1, OUTPUT);
  pinMode(ledPin2, OUTPUT);
  pinMode(ledPin3, OUTPUT);
  pinMode(ledPin4, OUTPUT);
}

void loop() {
  if (digitalRead(buttonPin1) == HIGH) {
    digitalWrite(ledPin1, HIGH);
    button1_state = true;
  }
  else {
    digitalWrite(ledPin1, LOW);
    if (button1_state) {
      Serial.println(1);
      Serial.flush();
      delay(20);
      button1_state = false;
    }
  }

  if (digitalRead(buttonPin2) == HIGH) {
    digitalWrite(ledPin2, HIGH);
    button2_state = true;
  }
  else {
    digitalWrite(ledPin2, LOW);
    if (button2_state) {
      Serial.println(2);
      Serial.flush();
      delay(20);
      button2_state = false;
    }
  }

  if (digitalRead(buttonPin3) == HIGH) {
    digitalWrite(ledPin3, HIGH);
    button3_state = true;
  }
  else {
    digitalWrite(ledPin3, LOW);
    if (button3_state) {
      Serial.println(3);
      Serial.flush();
      delay(20);
      button3_state = false;
    }
  }
  
  if (digitalRead(buttonPin4) == HIGH) {
    digitalWrite(ledPin4, HIGH);
    button4_state = true;
  }
  else {
    digitalWrite(ledPin4, LOW);
    if (button4_state) {
      Serial.println(4);
      Serial.flush();
      delay(20);
      button4_state = false;
    }
  }
}
