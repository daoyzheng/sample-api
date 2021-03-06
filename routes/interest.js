const router = require('koa-router')()
const { getInterests } = require('../controllers/interestController')

router.prefix('/interests')

router.get('/', getInterests)

module.exports = router