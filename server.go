package main

import (
	"encoding/json"
	"fmt"
	"io/ioutil"
	"net/http"
)

type girl struct {
	Name    string
	Car     string
	Country string
}

type girls []girl

func main() {
	http.HandleFunc("/", func(res http.ResponseWriter, req *http.Request) {
		res.Write([]byte("GOLANG"))
	})

	http.HandleFunc("/getstring", func(res http.ResponseWriter, req *http.Request) {
		if req.Method == "POST" {
			xx, _ := ioutil.ReadAll(req.Body)
			fmt.Println(string(xx))
		}
	})

	http.HandleFunc("/sendjson", func(res http.ResponseWriter, req *http.Request) {
		json.NewEncoder(res).Encode(&girl{Name: "Isabel", Car: "BMW", Country: "Spain"})
	})

	http.HandleFunc("/getjson", func(res http.ResponseWriter, req *http.Request) {
		if req.Method == "POST" {
			var girlTemp girl
			json.NewDecoder(req.Body).Decode(&girlTemp)
			fmt.Println(girlTemp.Name)
			fmt.Println(girlTemp.Car)
			fmt.Println(girlTemp.Country)
		}

	})

	http.HandleFunc("/sendjsonarray", func(res http.ResponseWriter, req *http.Request) {
		varTemp := girls{
			girl{Name: "Cristina", Car: "Seat", Country: "Germany"},
			girl{Name: "Raquel", Car: "Renault", Country: "Italy"},
		}
		json.NewEncoder(res).Encode(varTemp)
	})

	http.HandleFunc("/getjsonarray", func(res http.ResponseWriter, req *http.Request) {
		if req.Method == "POST" {
			var girlsTemp girls
			json.NewDecoder(req.Body).Decode(&girlsTemp)
			for _, v := range girlsTemp {
				fmt.Println(v.Name)
				fmt.Println(v.Car)
				fmt.Println(v.Country)
				fmt.Println()
			}
		}

	})

	http.HandleFunc("/getform", func(res http.ResponseWriter, req *http.Request) {
		if req.Method == "POST" {
			fmt.Println(req.FormValue("Name"))
			fmt.Println(req.FormValue("Car"))
		}

	})

	http.HandleFunc("/getquerystring", func(res http.ResponseWriter, req *http.Request) {
		val := req.URL.Query().Get("Name")
		fmt.Println(val)
	})

	http.ListenAndServe(":80", nil)

}
