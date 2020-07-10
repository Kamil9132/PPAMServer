# PPAMServer

## API Endpoints
* `/api/hospital`
	* Method: `GET`
	* Description:
	```
	Get list of hospitals
    ```
* `/hospital/{id}`
	* Method: `GET`
	* GET Parameters:
		* `id` Hospital id
	* Description:
	```
    Get hospital details (hospital info, scores, comments)
    For incorrect parameters 'Bad request' response is returned:
    {
        "message": "Bad Request"
    }
    ```
* `/hospital/{id}/score`
	* Method: `POST`
	* GET Parameters:
    	* `id` Hospital id
    * POST Parameters:
    	* `user` User id
    	* `score` Integer score
    * Description:
    ```
    Add hospital score
    Request returns same data as '/hospital/{id}'
    ```
* `/hospital/{id}/comment`
	* Method: `POST`
	* GET Parameters:
    	* `id` Hospital id
    * POST Parameters:
    	* `user` User id
    	* `comment` Comment, cannot be empty
    * Description:
    ```
    Add hospital comment
    Request returns same data as '/hospital/{id}'
    ```
* `/developer/replace-data`
	* Method: `POST`
    * POST Parameters:
    	* `developerKey` Developer key
    	* `data` Hospitals data
    * Description:
    ```
	Replace all database data with provided hospitals data (scores and comments are lost)
    ```

## Developer keys
* `wGKXKLf0bEUa7V8M4UC0olJymZ4p888h`