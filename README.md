# SSO Internal Memory Tool (v0yenka hax)

> **Target Version:** Better Star Stable (2017 Private Server Builds)  
> **Current Status:** Prototype / PoC  

## ⚠️ Disclaimer & TL;DR

Whatever you do with this code is all up to you, this project was created strictly for **educational purposes** to understand 32-bit process memory management and reverse engineering concepts.

---

## How it Works

This tool utilizes a custom-written C# library (`Mem.cs`) that acts as a wrapper for low-level Windows API calls (`kernel32.dll`). It allows for direct interaction with the game's memory space without injecting a DLL file, making it an external-internal hybrid.

### Key Features of the Engine:
* **Asynchronous AoB Scanning:** Uses `VirtualQueryEx` to map memory pages and scans for byte patterns (signatures) to locate dynamic addresses of game functions.
* **Non-Blocking UI:** The scanner runs on background threads using `Task.Run` and `ConcurrentBag` for thread-safe data collection, ensuring the GUI never freezes.
* **Memory Manipulation:** Uses `WriteProcessMemory` to inject custom internal game scripts (converted to byte arrays) directly into the execution flow.
* **P/Invoke Architecture:** Fully managed C# code interacting with unmanaged memory.

## Current GUI

**Note:** The version currently in this repository features a **basic Windows Forms GUI**. This is a temporary placeholder used for testing the memory library logic.

### COMING SOON
The project is undergoing a major overhaul. The upcoming V2 release will feature:

* **Innovative GUI:** A completely custom, borderless UI.
* **Script Hub:** A built-in library of scripts (Fly, Speed, NoClip, etc.) selectable from a list.
* **Web Integration:** Connection to an external website for fetching the latest community scripts.
* **Cosmetics Changer:** Features to change horse coats and player attributes locally.
* **Innovative UX:** Moving away from raw text injection to a user-friendly cheat panel.

## 💻 Tech Stack
* **Language:** C# (.NET Framework)
* **API:** Win32 API (User32.dll, Kernel32.dll)
* **IDE:** Visual Studio

---
## Author

**v0yenka**
