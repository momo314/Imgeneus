## Game Server Packets

### GAME_HANDSHAKE (0xA301)

#### Server -> Client

| Param | Size | Description |
| ----------- | ------------ | ----------- |
| Error | bool | If has an error return `true`, other wise return `false` and send faction packet. |

#### Client -> Server

| Param | Size | Description |
| ----------- | ------------ | ----------- |
| User id | int | The user unique id. |
| Identity Keys | byte[16] | The connection unique id assinged by the login server. |

### CHARACTER_LIST (0x0101)
#### Server -> Client

| Param | Size | Description |
| ----------- | ------------ | ----------- |
| Slot | byte | The character slot. |
| Character Id | int | The character unique id, sent 0 if the slot is empty, and then continue to the next slot. If the slot is not empty, send the character details. |
| Creation | int | The character creation timestamp. |
| Level | ushort | The Chatacter level. |
| Race | byte | The [Chatacter Race](https://github.com/KSExtrez/Imgeneus/blob/master/docs/structures/GameStructures.md#character-race). |
| Mode | byte | The [Chatacter mode](https://github.com/KSExtrez/Imgeneus/blob/master/docs/structures/GameStructures.md#character-mode). |
| Hair | byte | The character hair. |
| Face | byte | The Chatacter face. |
| Height | byte | The Chatacter height. |
| Class | byte | The Chatacter class. |
| Gender | byte | The Chatacter gender. |
| Map | ushort | The Chatacter spawn map. |
| Stats | ushort[9] | The Chatacter Stats, include Hp, Sp and Mp. |
| Types | byte[8] | The Chatacter equiping items types. |
| Type Ids | byte[8] | The Chatacter equiping items type ids. |
| Name | byte[19] | The Chatacter name. |
| Delete | bool | The Chatacter deletion flag. |
| Rename | bool | The Chatacter rename flag. |
| Cape emblems | byte[6] | The Chatacter equiping cape emblems. |

### CREATE_CHARACTER (0x0102)

### DELETE_CHARACTER (0x0103)

### SELECT_CHARACTER (0x0104)

### CHARACTER_DETAILS (0x0105)

### CHARACTER_INVENTORY (0x0106)

### CHARACTER_SKILLS (0x0108)

### ACCOUNT_FACTION (0x0109)

### CHARACTER_ACTIVE_SKILL (0x010A)

### CHARACTER_SKILL_BAR (0x010B)

### UNKNOW (0x010D)

### RENAME_CHARACTER (0x010E)

### RESTORE_CHARACTER (0x010F)

### UNKNOW (0x0110)

### EMPTY (0x0111)

### UNKNOW (0x0112)

### UNKNOW (0x0113)
