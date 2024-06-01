package event

import "time"

type TransactionCreated struct {
	Name     string
	DateTime time.Time
	Payload  interface{}
}

func NewTransactionCreated() *TransactionCreated {
	return &TransactionCreated{
		Name:     "TransactionCreated",
		DateTime: time.Now(),
	}
}

func (e *TransactionCreated) GetName() string {
	return e.Name
}

func (e *TransactionCreated) GetPayload() interface{} {
	return e.Payload
}

func (e *TransactionCreated) SetPayload(payload interface{}) {
	e.Payload = payload
}

func (e *TransactionCreated) GetDateTime() time.Time {
	return e.DateTime
}

func (e *TransactionCreated) SetDateTime(dateTime time.Time) {
	e.DateTime = dateTime
}
