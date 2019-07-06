## Shaiya Packets
In this documentation, you will find every Shaiya packet structure.

## Packet Structure

| Name | Data Type | Description |
| ----------- | ------------ | ----------- |
| Length | ushort | The packet total length. |
| Opcode | ushort | The packet operation code. |
| Arguments | undefined | The packet arguments. |

## Packet List

### Login

| Packet Name | Packet Value | Description |
| ----------- | ------------ | ----------- |
| [LOGIN_HANDSHAKE](/docs/packets/LOGIN.md#handshake-0xa101) | `0xA101` | Send a RSA public key, modulus and exponent. |
| [LOGIN_REQUEST](/docs/packets/LOGIN.md#login_request-0xa102) | `0xA102` | Request login to the server. |
| [SERVER_LIST](/docs/packets/LOGIN.md#server_list-0xa201) | `0xA201` | Send the list of available servers to the client. |
| [SELECT_SERVER](/docs/packets/LOGIN.md#select_server-0xa202) | `0xA202` | Send an error message to the client. |
| [LOGIN_TERMINATE](/docs/packets/LOGIN.md#Login_terminate-0x0107) | `0x0107` | Finish the login request. |

### Game

| Packet Name | Packet Value | Description |
| ----------- | ------------ | ----------- |
| [GAME_HANDSHAKE](/docs/packets/GAME.md#game_handshake-0xa301) | `0xA301` | Send user id and session identity keys from the login request. |

### Common Packets

### CLOSE_CONNECTION (0x0107)

This is an empty packet, used for terminate the client connection.
