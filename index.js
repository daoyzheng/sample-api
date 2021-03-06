const Koa = require('koa')
const bodyParser = require('koa-bodyparser')
const routes = require('./routes/index')

const app = new Koa()

app.use(bodyParser())
routes(app)

const port = process.env.PORT || 3000
app.listen(port, () => console.log(`App is running on port ${port}`))