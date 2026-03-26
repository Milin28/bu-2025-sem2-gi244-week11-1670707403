# Assignment 1 — สร้าง Animation ให้ Power indicator

ทำให้ Power indicator มี animation clip หมุนรอบตัวเองอย่างต่อเนื่อง

---

# Assignment 2 — สร้าง Power up: Enemy stun!

สร้าง Power up ที่เมื่อผู้เล่นเก็บแล้วจะทำให้ศัตรูทั้งหมดจะหยุดวิ่งตาม Player เป็นเวลา 5 วินาที

ข้อกำหนดเชิงเทคนิค

- สร้าง Power up ใหม่ (เช่น ชื่อ `StunPowerUp.cs`)
- อาจจะต้องแก้ไข Script `Enemy.cs` เพื่อเพิ่มฟังก์ชันการหยุดเคลื่อนที่ตาม Player

---

# Assignment 3 — ระบบ Wave Spawner (ใช้ Coroutine)

สร้างระบบ Wave Spawner ใน Unity โดยใช้ Coroutine

ภาพรวม

- จำนวน wave: 4
- จำนวน spawn points รวม: 6 (ชื่อ: SpawnPoint01 … SpawnPoint06)

ข้อกำหนดเชิงเทคนิค

- แต่ละ wave ต้องสุ่มเลือกชุด spawn points จำนวน `numberOfRandomSpawnPoint` เมื่อต้น wave เท่านั้น — หลังเลือกแล้ว ให้ spawn เฉพาะจากชุดที่เลือกนั้นตลอดทั้ง wave
- ฟิลด์ของแต่ละ wave:
  - `totalSpawnEnemies` (int): จำนวนศัตรูที่จะ spawn ใน wave
  - `numberOfRandomSpawnPoint` (int): จำนวน spawn points ที่สุ่มเลือกเมื่อต้น wave
  - `delayStart` (float, วินาที): หน่วงก่อนเริ่ม spawn ตัวแรกของ wave
  - `spawnInterval` (float, วินาที): เวลาระหว่างการ spawn แต่ละตัว (รองรับทศนิยม)
  - `numberOfPowerUp` (int): จำนวน PowerUp ที่ spawn ตอนเริ่ม wave (ตำแหน่งสุ่ม)

Behaviour / ข้อกำหนดพฤติกรรม

- เมื่อเริ่ม wave: spawn `numberOfPowerUp` powerups ทันที (ตำแหน่งสุ่ม)
- รอ `delayStart` แล้ว spawn ศัตรูทั้งหมด `totalSpawnEnemies` โดยใช้เฉพาะ spawn points ที่สุ่มเลือกไว้ และรอ `spawnInterval` ระหว่างแต่ละ spawn
- ห้ามให้ spawn ของ wave หนึ่งไหลไปยัง wave ถัดไป — ต้องทำให้ wave ปัจจุบันเสร็จก่อนจะเริ่ม wave ถัดไป (หรือระบุเงื่อนไขจบชัดเจน)

ค่าคอนฟิกของแต่ละ Wave

| Wave | totalSpawnEnemies | numberOfRandomSpawnPoint | delayStart (s) | spawnInterval (s) | numberOfPowerUp |
| ---: | ----------------: | -----------------------: | -------------: | ----------------: | --------------: |
|    1 |                 4 |                        1 |              2 |               2.0 |               0 |
|    2 |                 6 |                        2 |              2 |               2.0 |               1 |
|    3 |                 8 |                        4 |              2 |               2.0 |               1 |
|    4 |                10 |                        6 |              5 |               0.5 |               2 |

ตัวอย่างโครงสร้างข้อมูล (ตัวอย่าง C#)

```csharp
[System.Serializable]
public class Wave {
    public int totalSpawnEnemies;
    public int numberOfRandomSpawnPoint;
    public float delayStart;
    public float spawnInterval;
    public int numberOfPowerUp;
}
```

# การส่งงาน Code ผ่าน github repository

1. หลังจากที่ Fork Repository ตั้งชื่อ `bu-2025-sem2-gi244-week11-<student_id>` เช่น `bu-2025-sem2-gi244-week11-47050513`
2. สร้าง branch ชื่อ `assignment` commit code ลงใน branch `assignment` และ push branch `assignment` ขึ้นมาใน Fork Repository
3. File ที่จะเข้าไปตรวจให้คะแนน ครอบคลุมอย่างน้อย:
   - `Assets/Scripts/StunPowerUp.cs` (Assignment 2)
   - `Assets/Scripts/WaveSpawner.cs` (และไฟล์อื่นๆ ที่เกี่ยวข้อง ที่ reference จาก WaveSpawner) (Assignment 3)
   - ไฟล์อื่นๆ เช่น `Enemy.cs` หรือ `PlayerController.cs` ที่เกี่ยวข้องกับการทำงานของ Power up หรือ Wave Spawner ก็ได้

# การส่งงานผ่าน MS Team Assignment

1. file Video "Assignment1.mp4" ที่อัดหน้าจอการทำงานของ Assignment 1: จะต้องเห็น Power indicator หมุนรอบตัวเองอย่างต่อเนื่อง
2. file Video "Assignment2.mp4" ที่อัดหน้าจอการทำงานของ Assignment 2: จะต้องเห็น Player เก็บ Power up แล้วศัตรูหยุดวิ่งตาม Player เป็นเวลา 5 วินาที
3. file Video "Assignment3.mp4" ที่อัดหน้าจอการทำงานของ Assignment 3: จะต้องเห็นการ spawn ศัตรูและ Power up ตามค่าคอนฟิกของแต่ละ wave และต้องเห็นว่า wave ถัดไปเริ่มหลังจาก wave ปัจจุบันจบแล้วเท่านั้น ความยาวประมาณ 1 นาที (สามารถเร่งความเร็ว video ได้ถ้าต้องการ)
