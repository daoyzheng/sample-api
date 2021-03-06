const fs = require('fs')
const path = require('path')
const files = fs.readdirSync(__dirname)


module.exports = (app) => {
  files.forEach(file => {
    const fileEntity = require(path.join(__dirname, file))
    const fileName = file.substr(0, file.length - 3)
    if (fileName !== 'index') {
      app.use(fileEntity.routes())
    }
  })
}
