#include <TMC2209.h>
#include <SoftwareSerial.h>

//
//TMC2209 direction is opposite to A4988
//
#define PAUSE    A3
#define EN       2
#define X_STEP   4
#define X_DIR    3
#define Y_STEP   8
#define Y_DIR    7
#define Z_STEP   12
#define Z_DIR    11
#define A_STEP   A5
#define A_DIR    A4
#define B_STEP   10
#define B_DIR    9
#define C_STEP   6
#define C_DIR    5
#define SPEED    750
#define PAUSE_TIME 30
#define RX_PIN   A0
#define TX_PIN   A1
#define RUN_CURRENT_PERCENT   100
#define HOLD_CURRENT_PERCENT   70
#define HOLD_DELAY_CURRENT_PERCENT   50
#define MAX_ABC_STEP_COUNT   500
SoftwareSerial soft_serial(RX_PIN, TX_PIN);

byte cmdData[6];
int cmdByte = 0;
int count = 0;
char header;
int steps0;
int steps1;
bool originalMode = false;
int aStepCount;
int bStepCount;
int cStepCount;
int speed = SPEED;
int pausetime = PAUSE_TIME;

// Instantiate TMC2209
TMC2209 stepper_driver;

void doSteps(int pin, int steps) {
    for (int i = 0; i < steps; i++) {
        if (digitalRead(PAUSE)==LOW) return;
        digitalWrite(pin, HIGH);
        delayMicroseconds(speed);
        digitalWrite(pin, LOW);
        delayMicroseconds(speed);
    }
}

void setup() {

    pinMode(PAUSE, INPUT_PULLUP);
    pinMode(EN, OUTPUT);
    digitalWrite(EN, HIGH);
    pinMode(X_DIR, OUTPUT);
    pinMode(X_STEP, OUTPUT);
    pinMode(Y_DIR, OUTPUT);
    pinMode(Y_STEP, OUTPUT);
    pinMode(Z_DIR, OUTPUT);
    pinMode(Z_STEP, OUTPUT);
    pinMode(A_DIR, OUTPUT);
    pinMode(A_STEP, OUTPUT);
    pinMode(B_DIR, OUTPUT);
    pinMode(B_STEP, OUTPUT);
    pinMode(C_DIR, OUTPUT);
    pinMode(C_STEP, OUTPUT);


    // initialize serial:
    Serial.begin(115200);

    stepper_driver.setup(soft_serial);
}

void initTMC2209() {

    stepper_driver.setHardwareEnablePin(EN);
    //stepper_driver.enableCoolStep();
    //stepper_driver.setAllCurrentValues(RUN_CURRENT_PERCENT, HOLD_CURRENT_PERCENT, HOLD_DELAY_CURRENT_PERCENT);
    stepper_driver.setMicrostepsPerStep(2);
    //stepper_driver.enableStealthChop();
    stepper_driver.setStandstillMode(TMC2209::BRAKING);
    stepper_driver.useExternalSenseResistors();
    stepper_driver.enableAnalogCurrentScaling();//Vref=2.13V,Irms=0.707*2.13=1.5A
    stepper_driver.disableAutomaticCurrentScaling();
    //  stepper_driver.moveUsingStepDirInterface();
    stepper_driver.setPwmOffset(64);
    stepper_driver.setPwmGradient(64);
    stepper_driver.setRunCurrent(RUN_CURRENT_PERCENT);
    stepper_driver.setHoldCurrent(HOLD_CURRENT_PERCENT);
    stepper_driver.disable();

}

void loop() {


    if (Serial.available())
    {
        // get the new byte:
        cmdByte = Serial.read();
        count = count + 1;
        cmdData[count - 1] = cmdByte;
        if (cmdByte == '\n') // \0x0A
        {
            if (count == 6)
            {
                header = cmdData[0];
                steps0 = cmdData[1] << 8 | cmdData[2];
                steps1 = cmdData[3] << 8 | cmdData[4];
                //Serial.print(header);
                //Serial.print(" ");
                //Serial.print(steps0);
                //Serial.print(" ");
                //Serial.println(steps1);
                switch (header)
                {
                case 'E':
                    if (steps0 == 0) {
                        stepper_driver.disable();
                    }
                    else {
                        initTMC2209();
                        stepper_driver.enable();
                    }
                    break;
                case 'O':
                    if (steps0 == 0) {
                        originalMode = false;
                    }
                    else {
                        aStepCount = 0;
                        bStepCount = 0;
                        cStepCount = 0;
                        originalMode = true;
                    }
                    break;
                case 'S':
                    if (steps0 > 0) {
                        speed = steps0;
                    }
                    if (steps1 > 0) {
                        pausetime = steps1;
                    }
                    break;
                case 'X':
                    if (steps0 > 0) {
                        digitalWrite(X_DIR, LOW);
                        doSteps(X_STEP, steps0);
                    }
                    else if (steps0 < 0) {
                        digitalWrite(X_DIR, HIGH);
                        doSteps(X_STEP, -steps0);
                    }
                    delay(pausetime);
                    if (steps1 > 0) {
                        digitalWrite(X_DIR, LOW);
                        doSteps(X_STEP, steps1);
                    }
                    else if (steps1 < 0) {
                        digitalWrite(X_DIR, HIGH);
                        doSteps(X_STEP, -steps1);
                    }            
                    delay(pausetime);
                    break;
                case 'Y':
                    if (steps0 > 0) {
                        digitalWrite(Y_DIR, LOW);
                        doSteps(Y_STEP, steps0);
                    }
                    else if (steps0 < 0) {
                        digitalWrite(Y_DIR, HIGH);
                        doSteps(Y_STEP, -steps0);
                    }
                    delay(pausetime);
                    if (steps1 > 0) {
                        digitalWrite(Y_DIR, LOW);
                        doSteps(Y_STEP, steps1);
                    }
                    else if (steps1 < 0) {
                        digitalWrite(Y_DIR, HIGH);
                        doSteps(Y_STEP, -steps1);
                    }
                    delay(pausetime);
                    break;
                case 'Z':
                    if (steps0 > 0) {
                        digitalWrite(Z_DIR, LOW);
                        doSteps(Z_STEP, steps0);
                    }
                    else if (steps0 < 0) {
                        digitalWrite(Z_DIR, HIGH);
                        doSteps(Z_STEP, -steps0);
                    }
                    delay(pausetime);
                    if (steps1 > 0) {
                        digitalWrite(Z_DIR, LOW);
                        doSteps(Z_STEP, steps1);
                    }
                    else if (steps1 < 0) {
                        digitalWrite(Z_DIR, HIGH);
                        doSteps(Z_STEP, -steps1);
                    }
                    delay(pausetime);
                    break;
                case 'A':
                    if (originalMode) 
                    {
                        if (aStepCount + steps0 < 0) {
                            break;
                        }
                        else if (aStepCount + steps0 > MAX_ABC_STEP_COUNT)
                        {
                            break;
                        }
                        else
                        {
                            aStepCount += steps0;
                        }
                    }
                    if (steps0 > 0) {
                        digitalWrite(A_DIR, LOW);
                        doSteps(A_STEP, steps0);
                    }
                    else if (steps0 < 0) {
                        digitalWrite(A_DIR, HIGH);
                        doSteps(A_STEP, -steps0);
                    }
                    delay(pausetime);
                    break;
                case 'B':
                    if (originalMode)
                    {
                        if (bStepCount + steps0 < 0) {
                            break;
                        }
                        else if (bStepCount + steps0 > MAX_ABC_STEP_COUNT)
                        {
                            break;
                        }
                        else
                        {
                            bStepCount += steps0;
                        }
                    }
                    if (steps0 > 0) {
                        digitalWrite(B_DIR, LOW);
                        doSteps(B_STEP, steps0);
                    }
                    else if (steps0 < 0) {
                        digitalWrite(B_DIR, HIGH);
                        doSteps(B_STEP, -steps0);
                    }
                    delay(pausetime);
                    break;
                case 'C':
                    if (originalMode) 
                    {
                        if (cStepCount + steps0 < 0) {
                            break;
                        }
                        else if (cStepCount + steps0 > MAX_ABC_STEP_COUNT)
                        {
                            break;
                        }
                        else
                        {
                            cStepCount += steps0;
                        }
                    }
                    if (steps0 > 0) {
                        digitalWrite(C_DIR, LOW);
                        doSteps(C_STEP, steps0);
                    }
                    else if (steps0 < 0) {
                        digitalWrite(C_DIR, HIGH);
                        doSteps(C_STEP, -steps0);
                    }
                    delay(pausetime);
                    break;
                default:
                    break;
                }
                Serial.println("EOD");//End Of Do
            }
            count = 0;
            cmdData[0] = 0;
            cmdData[1] = 0;
            cmdData[2] = 0;
            cmdData[3] = 0;
            cmdData[4] = 0;
            cmdData[5] = 0;
        }
    }

}
