## Imgeneus Login structures

### World details
| Name | Data type | Description |
| ----------- | ------------ | ----------- |
| World ID | ushort | The connected `Imgeneus.World` unique id. |
| World Status | ushort | The `Imgeneus.World` current status (Close, Normal, Full, etc.). |
| World population | ushort | The number of people connected. |
| World Capacity | ushort | The number of maximum people connected allowed. |
| World Name | byte[32] | The `Imgeneus.World` Server name. |

### Login result
| Index | Name |
| ----------- | ------------ |
| 0 | Success |
| 1 | Account don't exists |
| 2 | Can't connect with the server |
| 3 | Invalid user or password |
| 4 | Can't connect to the game with this account |
| 5 | Can't connect to the game and web with this account |
| 6 | Username in delete process |
| 7 | Empty |
| 8 | Empty |
| 9 | Can't connect to game |
| 10 | Banned account |
| 11 | Restricted account |
| 12 | Empty |
| 13 | `null`(Error message 10105) |
| 14 | `null`(Error message 10104) |
| 15 | Deadline has expired. |

### Selec Server result

| Index | Name |
| ----------- | ------------ |
| 0 | Success |
| -1 | Try again later |
| -2 | Can not connect |
| -3 | Version Doesn't Match |
| -4 | Server Saturated |
