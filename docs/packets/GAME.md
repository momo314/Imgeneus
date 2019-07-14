## Game Server Packets

### GAME_HANDSHAKE (0xA301)

#### Server -> Client

| Param | Size | Description |
| ----------- | ------------ | ----------- |
| Error | bool | If has an error return `true`, otherwise return `false` and send faction packet. |

#### Client -> Server

| Param | Size | Description |
| ----------- | ------------ | ----------- |
| User id | int | The user unique id. |
| Identity Keys | byte [16] | The connection unique id assigned by the login server. |

### CHARACTER_LIST (0x0101)

#### Server -> Client

| Param | Size | Description |
| ----------- | ------------ | ----------- |
| Slot | byte | The character slot. |
| Character Id | int | The character unique id, sent 0 if the slot is empty, and then continue to the next slot. If the slot is not empty, send the character details. |
| Creation | int | The character creation timestamp. |
| Level | ushort | The Character level. |
| Race | byte | The [Character Race](https://github.com/KSExtrez/Imgeneus/blob/master/docs/structures/GameStructures.md#character-race). |
| Mode | byte | The [Character mode](https://github.com/KSExtrez/Imgeneus/blob/master/docs/structures/GameStructures.md#character-mode). |
| Hair | byte | The character hair. |
| Face | byte | The Character face. |
| Height | byte | The Character height. |
| Class | byte | The Character class. |
| Gender | byte | The Character gender. |
| Map | ushort | The Character spawn map. |
| Stats | ushort [9] | The Character Stats, include Hp, Sp and Mp. |
| Types | byte [8] | The Character equipping items types. |
| Type Ids | byte [8] | The Character equipping items type ids. |
| Name | byte [19] | The Character name. |
| Delete | bool | The Character deletion flag. |
| Rename | bool | The Character rename flag. |
| Cape emblems | byte [6] | The Character equipping cape emblems. |

### CREATE_CHARACTER (0x0102)

#### Server -> Client

| Param | Size | Description |
| ----------- | ------------ | ----------- |
| Error | bool | If has an error return `true`, otherwise return `false` and send [Character List](https://github.com/KSExtrez/Imgeneus/blob/master/docs/packets/GAME.md#character_list-0x0101). |

#### Client -> Server

| Param | Size | Description |
| ----------- | ------------ | ----------- |
| Slot | byte | The character free slot. |
| Race | byte | The [Character Race](https://github.com/KSExtrez/Imgeneus/blob/master/docs/structures/GameStructures.md#character-race). |
| Mode | byte | The [Character mode](https://github.com/KSExtrez/Imgeneus/blob/master/docs/structures/GameStructures.md#character-mode).  |
| Hair | byte | The character free hair. |
| Face | byte | The character free face. |
| Height | byte | The character free height. |
| Class | byte | The Character class. |
| Gender | byte | The Character gender. |
| Name | byte [19] | The Character name. |

### DELETE_CHARACTER (0x0103)

#### Server -> Client

| Param | Size | Description |
| ----------- | ------------ | ----------- |
| Error | bool | If has an error return `true`, otherwise return `false` and send the character delete id |
| Character Id | int | The character delete id |

#### Client -> Server

| Param | Size | Description |
| ----------- | ------------ | ----------- |
| Character Id | int | The character id to delete. |

### SELECT_CHARACTER (0x0104)

#### Server -> Client
| Param | Size | Description |
| ----------- | ------------ | ----------- |
| Error | bool | If has an error return `true`, otherwise return `false` and send the character delete id |
| Character Id | int | The character id |

#### Client -> Server

| Param | Size | Description |
| ----------- | ------------ | ----------- |
| Character Id | int | The character id for play. |

### CHARACTER_DETAILS (0x0105)

### CHARACTER_INVENTORY (0x0106)

### CHARACTER_SKILLS (0x0108)

### ACCOUNT_FACTION (0x0109)

#### Server -> Client

| Param | Size | Description |
| ----------- | ------------ | ----------- |
| Faction | byte | The [Account Faction](https://github.com/KSExtrez/Imgeneus/blob/master/docs/structures/GameStructures.md#account-faction). |
| Mode | byte | The maximum allowed [Character mode](https://github.com/KSExtrez/Imgeneus/blob/master/docs/structures/GameStructures.md#character-mode).  |


### CHARACTER_ACTIVE_SKILL (0x010A)

### CHARACTER_SKILL_BAR (0x010B)

### UNKNOW (0x010D)

### RENAME_CHARACTER (0x010E)

### RESTORE_CHARACTER (0x010F)

#### Server -> Client

| Param | Size | Description |
| ----------- | ------------ | ----------- |
| Error | bool | If has an error return `true`, otherwise return `false` and send the character delete id |
| Character Id | int | The restored character id. |

#### Client -> Server

| Param | Size | Description |
| ----------- | ------------ | ----------- |
| Character Id | int | The character id to restore. |

### UNKNOW (0x0110)

### EMPTY (0x0111)

### UNKNOW (0x0112)

### UNKNOW (0x0113)
