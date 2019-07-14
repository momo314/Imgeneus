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

### Game

| Packet Name | Packet Value | Description |
| ----------- | ------------ | ----------- |
| [GAME_HANDSHAKE](/docs/packets/GAME.md#game_handshake-0xa301) | `0xA301` | Send user id and session identity keys from the login request. |
| [CHARACTER_LIST](/docs/packets/GAME.md#character_list-0x0101) | `0x0101` | A list of character in this account. |
| [CREATE_CHARACTER](https://github.com/KSExtrez/Imgeneus/blob/master/docs/packets/GAME.md#create_character-0x0102) | `0x0102` | Creates a new character. |
| [DELETE_CHARACTER](https://github.com/KSExtrez/Imgeneus/blob/master/docs/packets/GAME.md#delete_character-0x0103) | `0x0103` | Delete a character. |
| [SELECT_CHARACTER](https://github.com/KSExtrez/Imgeneus/blob/master/docs/packets/GAME.md#select_character-0x0104) | `0x0104` | Select the character for start to play. |
| [CHARACTER_DETAILS](https://github.com/KSExtrez/Imgeneus/blob/master/docs/packets/GAME.md#character_details-0x0105) | `0x0105` |The character details. |
| [CHARACTER_INVENTORY ](https://github.com/KSExtrez/Imgeneus/blob/master/docs/packets/GAME.md#character_inventory-0x0106) | `0x0106` | A list of character items. |
| [CHARACTER_SKILLS](https://github.com/KSExtrez/Imgeneus/blob/master/docs/packets/GAME.md#character_skills-0x0108) | `0x0108` | A list of character skills. |
| [ACCOUNT_FACTION](https://github.com/KSExtrez/Imgeneus/blob/master/docs/packets/GAME.md#account_faction-0x0109) | `0x0109` | The [Account Faction](https://github.com/KSExtrez/Imgeneus/blob/master/docs/structures/GameStructures.md#account-faction). |
| [CHARACTER_ACTIVE_SKILL](https://github.com/KSExtrez/Imgeneus/blob/master/docs/packets/GAME.md#character_active_skill-0x010a) | `0x010A` | A list of character active skills. |
| [CHARACTER_SKILL_BAR](https://github.com/KSExtrez/Imgeneus/blob/master/docs/packets/GAME.md#character_skill_bar-0x010b) | `0x010B` | A list of character items or skills in the skill bar. |
| [RENAME_CHARACTER](https://github.com/KSExtrez/Imgeneus/blob/master/docs/packets/GAME.md#rename_character-0x010e) | `0x010E` | Rename the character. |
| [RESTORE_CHARACTER ](https://github.com/KSExtrez/Imgeneus/blob/master/docs/packets/GAME.md#restore_character-0x010f) | `0x010F` | Restore the character. |


### Common Packets

#### CLOSE_CONNECTION (0x0107)

This is an empty packet, used for terminate the character connection.

#### PING (0x0003)

This is an empty packet used as a keepalive to ensure the client is still connected to the server.
