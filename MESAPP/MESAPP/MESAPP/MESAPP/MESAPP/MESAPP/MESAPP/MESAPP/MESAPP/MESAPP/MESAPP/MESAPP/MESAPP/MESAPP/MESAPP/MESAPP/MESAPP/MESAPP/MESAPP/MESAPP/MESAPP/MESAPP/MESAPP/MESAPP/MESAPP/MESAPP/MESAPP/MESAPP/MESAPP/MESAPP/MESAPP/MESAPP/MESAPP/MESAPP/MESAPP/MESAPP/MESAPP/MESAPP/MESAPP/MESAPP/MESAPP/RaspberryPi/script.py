import serial
import os
import time
import json
import paho.mqtt.client as mqtt
from datetime import datetime

# ✅ Configuration section
CONFIG = {
    'serial_port': '/dev/ttyUSB0',
    'baud_rate': 9600,
    'timeout': 1,
    'mqtt_broker': '192.168.1.102',
    'mqtt_port': 1883,
    'mqtt_topic': 'factory/workstation/1/sensors',
    'workstation_id': 1
}

# ✅ Create MQTT connection
client = mqtt.Client()
client.connect(CONFIG['mqtt_broker'], CONFIG['mqtt_port'], 60)

# ✅ Main program
def main():
    try:
        if not os.path.exists(CONFIG['serial_port']):
            print("🚨 Serial port not found!")
            return

        ser = serial.Serial(CONFIG['serial_port'], CONFIG['baud_rate'], timeout=CONFIG['timeout'])
        print(f"✅ Serial port {CONFIG['serial_port']} connected.")

        while True:
            readed_text = ser.readline().decode('utf-8').strip()
            if not readed_text:
                continue

            print(f"🧾 Read data: {readed_text}")

            if readed_text.isdigit():
                value = int(readed_text)

                payload = {
                    "workstationId": CONFIG['workstation_id'],
                    "timestamp": datetime.utcnow().isoformat() + "Z",
                    "measurements": [
                        {
                            "sensorTypeId": 1,
                            "value": value
                        }
                    ]
                }

                client.publish(CONFIG['mqtt_topic'], json.dumps(payload))
                print("📤 MQTT message sent.")

            time.sleep(1)

    except serial.SerialException as se:
        print(f"🚨 Serial port error: {se}")
    except KeyboardInterrupt:
        print("\n🔴 Program interrupted...")
    finally:
        if 'ser' in locals() and ser.is_open:
            ser.close()
        client.disconnect()

if __name__ == "__main__":
    main()
