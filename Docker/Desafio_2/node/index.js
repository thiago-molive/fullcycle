const express = require('express')
const app = express()
const port = 3000
const config = {
    host: 'db',
    user: 'root',
    password: 'minhasupersenharoots',
    database:'fullcycle'
};
const mysql = require('mysql')

app.get('/', (req, res) => {
    const connection = mysql.createConnection(config)
    connection.query(`SELECT username FROM users`, (error, results, fields) => {
      res.send(`
        <h1>Full Cycle Rocks!</h1>
        <ol>
          ${results && results.length ? results.map(el => `<li>${el.username}</li>`).join('') : 'Wait and refresh page...'}
        </ol>
      `)
    })

    connection.end()
  })



app.listen(port, ()=> {
    console.log('Rodando na porta ' + port)
})