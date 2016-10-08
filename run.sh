#!/bin/sh

DATABASE=ValuesDb
USERNAME=ValuesUser
PASSWORD=
ADDRESS=

if [ "$1" != "-f" ]; then
    echo "Use -f option to actually run it."
    exit 0
fi

TO=
CC=
SUBJECT=$ADDRESS", показания приборов учёта"
BODY="Показания приборов учёта. "$ADDRESS".\n"`psql -A -t -d $DATABASE -U $USERNAME -v PGPASSWORD=$PASSWORD -c SELECT\ \"Day\",\ \"Night\",\ \"Cold\",\ \"Hot\"\ FROM\ \"CurrentValues\" | sed -e 's/\([0-9][0-9]*\)\x7C\([0-9][0-9]*\)\x7C\([0-9][0-9]*\)\x7C\([0-9][0-9]*\)/День: \1\x2C ночь: \2\x2C хол: \3\x2C гор: \4./g'`"\n\nP.S. Перешлите, пожалуйста, счёт-фактуру. Спасибо!"

echo -e $BODY | mail -c $CC -s "$SUBJECT" $TO
