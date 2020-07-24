# PPAMServer

## Data Types
* `HospitalsContainer`
```
{
	hospitals: Hospital[]
}
```
* `Hospital`
```
{
	id: string
    name: string
    description: string
    location: Location
}
```
* `HospitalDetails`
```
{
	id: string
    name: string
    description: string
    location: Location
    comments: Comment[]
    scores: Score[]
}
```
* `Location`
```
{
	lat: double
    lng: double
}
```
* `Comment`
```
{
	id: string
    hospitalId: string
    user: string
    comment: string
    dateUTC: string (date in ISO 8601 format)
}
```
* `Score`
```
{
	id: ScoreId
    score: int
    dateUTC: string (date in ISO 8601 format)
}
```
* `ScoreId`
```
{
	hospitalId: string
    user: string
}
```

## API Endpoints
* `/api/hospital`
	* Method: `GET`
    * Response: `HospitalsContainer`
	* Description:
	```
	Get list of hospitals
    ```
* `/hospital/{id}`
	* Method: `GET`
	* GET Parameters:
		* `id` Hospital id
    * Response: `HospitalDetails`
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
    * Response: `HospitalDetails`
    * Description:
    ```
    Add hospital score
    Request returns same data as '/hospital/{id}'
    ```
    * Example POST Data:
    ```
    {
        "user": "User001",
        "score": 5
    }
    ```
* `/hospital/{id}/comment`
	* Method: `POST`
	* GET Parameters:
    	* `id` Hospital id
    * POST Parameters:
    	* `user` User id
    	* `comment` Comment, cannot be empty
    * Response: `HospitalDetails`
    * Description:
    ```
    Add hospital comment
    Request returns same data as '/hospital/{id}'
    ```
    * Example POST Data:
    ```
    {
    	"user": "User001",
        "comment": "First"
    }
    ```
* `/developer/replace-data`
	* Method: `POST`
    * POST Parameters:
    	* `developerKey` Developer key
    	* `data` Hospitals data
	* Response: `Hospital[]`
    * Description:
    ```
	Replace all database data with provided hospitals data (scores and comments are lost)
    ```

## Developer keys
* `wGKXKLf0bEUa7V8M4UC0olJymZ4p888h`