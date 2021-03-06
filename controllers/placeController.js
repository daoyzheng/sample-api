const {
  get
} = require('../services/placeService')
module.exports = {
  getPlaces: (ctx) => {
    const { search, type } = ctx.query
    const data = get({ search, type })
    ctx.body = data
  }
}