const fs = require('fs')
const files = fs.readdirSync('resources')

const resources = []

files.forEach(file => {
  const { data } = require(`../resources/${file}`)
  resources.push(...data)
})

module.exports = {
  get: (search) => {
    if (search) {
      return resources.filter(resource => resource.Label.toLowerCase().includes(search.toLowerCase()))
    }
    return resources
  }
}