from flask import Flask, jsonify

app = Flask(__name__)

# List of products
products = [
	{"id": 1, "name": "Product A", "description": "Description of product A", "price": 10.99},
	{"id": 2, "name": "Product B", "description": "Description of product B", "price": 12.99},
	{"id": 3, "name": "Product C", "description": "Description of product C", "price": 15.99},
	{"id": 4, "name": "Product D", "description": "Description of product D", "price": 9.99},
	{"id": 5, "name": "Product E", "description": "Description of product E", "price": 20.99},
	{"id": 6, "name": "Product F", "description": "Description of product F", "price": 5.99},
	{"id": 7, "name": "Product G", "description": "Description of product G", "price": 7.99},
	{"id": 8, "name": "Product H", "description": "Description of product H", "price": 8.99},
	{"id": 9, "name": "Product I", "description": "Description of product I", "price": 11.99}
]

@app.route('/api/products', methods=['GET'])
def get_products():
	"""Endpoint to return list of products."""
	return jsonify(products)

if __name__ == '__main__':
	app.run(debug=True)