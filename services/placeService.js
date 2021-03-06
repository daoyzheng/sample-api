const fs = require('fs')
const files = fs.readdirSync('resources')

const resources = []

files.forEach(file => {
  if (file.includes('Places')) {
    const { data } = require(`../resources/${file}`)
    resources.push(...data)
  }
})

module.exports = {
  get: ({ search, type }) => {
    let data = resources
    if (search) {
      data = data.filter(resource => resource.name.toLowerCase().includes(search.toLowerCase()))
    }
    if (type) {
      data = data.filter(resource => resource.type.toLowerCase().includes(type.toLowerCase()))
    }
    return data
  }
}
