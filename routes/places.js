const router = require('koa-router')()
const { getPlaces } = require('../controllers/placeController')

router.prefix('/places')

router.get('/', getPlaces)

module.exports = router
