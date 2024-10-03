# InV6

InV6 (Invisix) is a **remote administration tool** that operates invisibly in the background of a system. It currently features a **Terminator**, a module capable of stopping applications added to its blacklist, as soon as they start running. This functionality is useful for terminating specific programs on a target system. Future updates will add more advanced features such as file encryption, keylogging, and system locking.

> **Note**: This project is intended for educational and ethical security research purposes only. Unauthorized use of this software is strictly prohibited.

## Features

- **Hidden Service**: Runs invisibly in the background without detection.
- **Remote Command Execution**: Fetches commands from a REST API service.
- **Terminator**: A blacklist-based feature that stops specific programs from running by terminating them upon startup.
- **Configurable Server Address**: The server address can be customized for different targets.

### Planned Features:
- **File Encryption**: Encrypt files on the target system until a password is provided.
- **Keylogging**: Record all keystrokes from the target system.
- **System Lock Screen**: Disable user access by killing critical processes like `explorer.exe` and preventing task manager from starting.
- **Advanced File Management**: Remotely upload, download, and manage files on the target machine.
