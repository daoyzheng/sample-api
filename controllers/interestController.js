const {
  get
} = require('../services/interestService')
module.exports = {
  getInterests: (ctx) => {
    const { search } = ctx.query
    const data = get(search)
    ctx.body = data
  }
}