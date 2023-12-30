# User API

- [User API](#buber-user-api)
  - [Create User](#create-user)
    - [Create User Request](#create-user-request)
    - [Create User Response](#create-user-response)
  - [Get User](#get-user)
    - [Get User Request](#get-user-request)
    - [Get User Response](#get-user-response)
  - [Update User](#update-user)
    - [Update User Request](#update-user-request)
    - [Update User Response](#update-user-response)
  - [Delete User](#delete-user)
    - [Delete User Request](#delete-user-request)
    - [Delete User Response](#delete-user-response)

## Create User

### Create User Request

```js
POST /users
```

```json
{
    "username": "zivojindavidovic",
    "email": "zivojindavidovic@gmail.com",
    "password": "zivojindavidovicpassword",
}
```

### Create User Response

```js
201 Created
```

```yml
Location: {{host}}/users/{{id}}
```

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "username": "zivojindavidovic",
    "email": "zivojindavidovic@gmail.com"   
}
```

## Get User

### Get User Request

```js
GET /users/{{id}}
```

### Get User Response

```js
200 Ok
```

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "username": "zivojindavidovic",
    "email": "zivojindavidovic@gmail.com"
}
```

## Update User

### Update User Request

```js
PUT /users/{{id}}
```

```json
{
    "username": "zivojindavidovic",
    "email": "zivojindavidovic@gmail.com",
    "password": "zivojindavidovicpassword"
}
```

### Update User Response

```js
204 No Content
```

or

```js
201 Created
```

```yml
Location: {{host}}/users/{{id}}
```

## Delete User

### Delete User Request

```js
DELETE /users/{{id}}
```

### Delete User Response

```js
204 No Content
```