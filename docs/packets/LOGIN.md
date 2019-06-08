## Login Server packets

### LOGIN_HANDSHAKE (0xA101)

#### Server -> Client

| Param | Size | Description |
| ----------- | ------------ | ----------- |
| Unknow | byte | Unknow param. |
| Exponent size | byte | The RSA public exponent size. |
| Modulus size | byte | The RSA public modulus size. |
| RSA Exponent | Exponent size | The RSA public exponent. |
| RSA Modulus | Modulus size | The RSA public modulus. |

#### Client -> Server

| Param | Size | Description |
| ----------- | ------------ | ----------- |
| Message size | byte | The RSA encrypted message size. |
| Encrypted message | Message size | The RSA encrypted message. |

### LOGIN_REQUEST (0xA102)

#### Server -> Client

| Param | Size | Description |
| ----------- | ------------ | ----------- |
| Login result | byte | The [Login Request Result](https://github.com/KSExtrez/Imgeneus/blob/master/docs/structures/LOGIN.md#login-result). |
| User id | int | The user unique id. |
| User status | byte | The user current status (GM, Banned, etc.). |
| Identity Keys | byte[16] | The connection unique id. |

#### Client -> Server

| Param | Size | Description |
| ----------- | ------------ | ----------- |
| Username | byte[19] | The user name. |
| Unknow | byte[13] | Unknow param. |
| Password | byte[16] | The user password. |

### SERVER_LIST (0xA201)

#### Server -> Client

| Param | Size | Description |
| ----------- | ------------ | ----------- |
| World count | byte | The number of `Imgeneus.World` Servers connected. |
| World details | List<WorldDetails> | A List of [World details](https://github.com/KSExtrez/Imgeneus/blob/master/docs/structures/LOGIN.md#world-details)  |


### SELECT_SERVER (0xA202)

#### Server -> Client

| Param | Size | Description |
| ----------- | ------------ | ----------- |
| Select Server Result | byte | The select [Server Request Result](https://github.com/KSExtrez/Imgeneus/blob/master/docs/structures/LOGIN.md#selec-server-result). |
| World Server IP | byte[4] | The `Imgeneus.World` IP Address bytes. |

#### Client -> Server

| Param | Size | Description |
| ----------- | ------------ | ----------- |
| World ID | byte | The connected `Imgeneus.World` unique id. |
| Client Version | int | The client current build version. |

### LOGIN_TERMINATE (0x010B)

This is an empty packet, used for terminate the client connection.
